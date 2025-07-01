using Random = UnityEngine.Random;

namespace Game
{
    public class MoveToStationState : FsmState
    {
        private readonly DroneView _view;
        private readonly DronesModel _drones;
        private readonly StationsModel _stations;
        private readonly string _droneId;

        public MoveToStationState(
            Fsm fsm, DroneView view, DronesModel drones, StationsModel stations, string droneId) : base(fsm)
        {
            _view = view;
            _drones = drones;
            _stations = stations;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();
            _drones.ChangeState(_droneId, DroneState.Move);

            _view.SetAvoidancePriority(Random.Range(30, 60));

            var stationId = _drones.GetStationId(_droneId);
            var stationPos = _stations.GetPosition(stationId);
            _view.MoveTo(stationPos, TransferCargo);
        }

        private void TransferCargo()
        {
            _fsm.SetState<CargoTransferState>();
        }
    }
}
