using UnityEngine;

namespace Game
{
    public class DroneIdleState : FsmState
    {
        private readonly DroneView _view;
        private readonly DronesModel _drones;
        private readonly SpaceResModel _resources;
        private readonly string _droneId;

        public DroneIdleState(Fsm fsm, DroneView view, DronesModel drones, SpaceResModel resources, string droneId) : base(fsm)
        {
            _view = view;
            _drones = drones;
            _resources = resources;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();

            _drones.ChangeState(_droneId, DroneState.Idle);
            _view.SetAvoidancePriority(0);

            _drones.OnTargetChanged += Drone_OnTargetChanged;
            Drone_OnTargetChanged(_droneId);
        }

        public override void Exit()
        {
            base.Exit();
            _drones.OnTargetChanged -= Drone_OnTargetChanged;
        }

        public override void Update()
        {
            base.Update();
            FindNearestFreeRes();
        }

        private void Drone_OnTargetChanged(string obj)
        {
            if (obj != _droneId)
                return;

            var targetResId = _drones.GetTargetResource(_droneId);
            if (string.IsNullOrEmpty(targetResId))
                return;

            _fsm.SetState<MoveToResourceState>();
        }

        private void FindNearestFreeRes()
        {
            var minDistance = float.MaxValue;
            var targetResId = string.Empty;

            foreach (var resId in _resources.Resources)
            {
                if (!_resources.IsFree(resId))
                    continue;

                var resPos = _resources.GetPosition(resId);
                var dronePos = _view.transform.position;

                var dist = Vector3.Distance(dronePos, resPos);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    targetResId = resId;
                }
            }

            if (string.IsNullOrEmpty(targetResId))
                return;

            _resources.Reserve(targetResId);
            _drones.SetTargetResource(_droneId, targetResId);
            _fsm.SetState<MoveToResourceState>();
        }
    }
}
