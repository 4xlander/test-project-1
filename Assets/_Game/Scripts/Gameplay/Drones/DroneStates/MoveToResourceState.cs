using Random = UnityEngine.Random;

namespace Game
{
    public class MoveToResourceState : FsmState
    {
        private readonly DroneView _view;
        private readonly DronesModel _drones;
        private readonly SpaceResModel _resources;
        private readonly string _droneId;

        private string _targetResId;

        public MoveToResourceState(
            Fsm fsm, DroneView view, DronesModel drones, SpaceResModel resources, string droneId) : base(fsm)
        {
            _view = view;
            _drones = drones;
            _resources = resources;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();
            _drones.ChangeState(_droneId, DroneState.Move);
            _targetResId = string.Empty;
            _view.SetAvoidancePriority(Random.Range(30, 60));
        }

        public override void Update()
        {
            base.Update();
            MoveToTargetRes();
        }

        private void MoveToTargetRes()
        {
            var resId = _drones.GetTargetResource(_droneId);
            if (string.IsNullOrEmpty(resId) || resId == _targetResId)
                return;

            _targetResId = resId;
            var targetPos = _resources.GetPosition(_targetResId);
            _view.MoveTo(targetPos, GatherResource);
        }

        private void GatherResource()
        {
            _fsm.SetState<GatheringState>();
        }
    }
}
