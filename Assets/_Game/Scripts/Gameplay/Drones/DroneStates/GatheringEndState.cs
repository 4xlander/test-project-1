using UnityEngine;

namespace Game
{
    public class GatheringEndState : FsmState
    {
        private const float WAIT_TIME = 2f;

        private readonly DronesModel _drones;
        private readonly string _droneId;

        private float _timer;

        public GatheringEndState(Fsm fsm, DronesModel drones, string droneId) : base(fsm)
        {
            _drones = drones;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();
            _drones.ChangeState(_droneId, DroneState.Wait);

            _timer = WAIT_TIME;
        }

        public override void Update()
        {
            base.Update();
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _fsm.SetState<MoveToStationState>();
            }
        }
    }
}
