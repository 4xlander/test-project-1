using UnityEngine;

namespace Game
{
    public class CargoTransferState : FsmState
    {
        private const float TRANSFER_CARGO_TIME = 1f;

        private readonly DroneView _view;
        private readonly DronesModel _drones;
        private readonly StationsModel _stations;
        private readonly string _droneId;

        private float _timer;

        public CargoTransferState(
            Fsm fsm,
            DroneView view,
            DronesModel drones,
            StationsModel stations,
            string droneId) : base(fsm)
        {
            _view = view;
            _drones = drones;
            _stations = stations;
            _droneId = droneId;
        }

        public override void Enter()
        {
            base.Enter();

            _drones.ChangeState(_droneId, DroneState.CargoTransfer);

            _timer = TRANSFER_CARGO_TIME;
            _view.SetAvoidancePriority(0);
        }

        public override void Update()
        {
            base.Update();

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                TransferCargoToStation();
            }
        }

        private void TransferCargoToStation()
        {
            var cargo = _drones.GetCargo(_droneId);
            var stationId = _drones.GetStationId(_droneId);
            foreach (var item in cargo)
                _stations.TransferCargo(stationId, item);

            _drones.ClearCargo(_droneId);
            _fsm.SetState<DroneIdleState>();
        }
    }
}
