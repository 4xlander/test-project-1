using UnityEngine;

namespace Game
{
    public class GatheringState : FsmState
    {
        private const float GATHER_TIME = 1f;

        private readonly DroneView _view;
        private readonly DronesModel _drones;
        private readonly DroneConfig _config;
        private readonly SpaceResModel _resources;
        private readonly string _droneId;

        private float _timer;

        public GatheringState(
            Fsm fsm,
            DroneView view,
            DronesModel drones,
            DroneConfig config,
            SpaceResModel resources,
            string droneId) : base(fsm)
        {
            _view = view;
            _drones = drones;
            _config = config;
            _resources = resources;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();
            _drones.ChangeState(_droneId, DroneState.Gather);

            _timer = GATHER_TIME;
            _view.SetAvoidancePriority(0);
        }

        public override void Update()
        {
            base.Update();

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = GATHER_TIME;
                GatherResource();
            }
        }

        private void GatherResource()
        {
            var resId = _drones.GetTargetResource(_droneId);
            var gatheredValue = _resources.Gather(resId, _config.GatherSpeedBase);

            if (gatheredValue > 0)
                _drones.AddCargo(_droneId, _resources.GetResType(resId), gatheredValue);
            else
            {
                _drones.SetTargetResource(_droneId, string.Empty);
                _resources.RemoveRes(resId);
                _fsm.SetState<GatheringEndState>();
            }
        }
    }
}
