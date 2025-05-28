namespace Game
{
    public class StationController : ITickable
    {
        private readonly string _stationId;
        private readonly StationsModel _model;
        private readonly StationView _view;
        private readonly DronesModel _drones;
        private readonly DroneFactory _droneFactory;
        private readonly TickManager _tickManager;

        public StationController(
            string stationId,
            StationsModel model, StationView view, DronesModel drones,
            DroneFactory droneFactory, TickManager tickManager)
        {
            _stationId = stationId;
            _model = model;
            _view = view;
            _drones = drones;
            _droneFactory = droneFactory;
            _drones.OnDroneRemoved += Drones_OnDroneRemoved;

            _tickManager = tickManager;
            _tickManager.Register(this);
        }

        private void Drones_OnDroneRemoved(string obj)
        {
            _model.RemoveDrone(_stationId, obj);
        }

        public void Tick()
        {
            var maxDronesCount = _model.GetMaxDronesCount(_stationId);
            var drones = _model.GetDrones(_stationId);

            if (drones.Count < maxDronesCount)
            {
                var droneId = _droneFactory.CreateBasicDrone(_stationId, _view.DroneSpawnPoint);
                _model.AddDrone(_stationId, droneId);
            }

            while (drones.Count > maxDronesCount)
            {
                _drones.RemoveDrone(drones[drones.Count - 1]);
            }
        }
    }
}
