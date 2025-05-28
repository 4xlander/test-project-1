namespace Game
{
    public class GameplayUIController
    {
        private GameplayUIView _view;
        private StationsModel _stationsModel;
        private DronesModel _dronesModel;
        private SpaceResSpawnerModel _spaceResSpawner;

        public GameplayUIController(
            GameplayUIView view, StationsModel stationsModel, DronesModel dronesModel, SpaceResSpawnerModel spaceResSpawner)
        {
            _view = view;
            _stationsModel = stationsModel;
            _dronesModel = dronesModel;
            _spaceResSpawner = spaceResSpawner;
        }

        public void Init()
        {
            InitDronesCountControl();
            InitDronesSpeedControl();
            InitStationResourceInfo();
            InitSpaceResSpawnControl();
            InitDronePathVisibilityControl();
        }

        private void InitDronePathVisibilityControl()
        {
            _view.OnDronePathVisibilityChanged += View_OnDronePathVisibilityChanged;
        }

        private void View_OnDronePathVisibilityChanged(bool value)
        {
            _dronesModel.SetPathVisibility(value);
        }

        private void InitDronesSpeedControl()
        {
            _view.OnDroneSpeedChanged += View_OnDroneSpeedChanged;
        }

        private void View_OnDroneSpeedChanged(float value)
        {
            foreach (var drone in _dronesModel.Drones)
                _dronesModel.SetSpeed(drone, value);
        }

        private void InitSpaceResSpawnControl()
        {
            _view.SetSpawnInterval(_spaceResSpawner.SpawnInterval);
            _view.OnSpaceResSpawnIntervalChanged += View_OnSpaceResSpawnIntervalChanged;
        }

        private void View_OnSpaceResSpawnIntervalChanged(float obj)
        {
            _spaceResSpawner.SpawnInterval = obj;
        }

        private void InitStationResourceInfo()
        {
            foreach (var station in _stationsModel.Stations)
                _view.AddResourceInfo(station, _stationsModel.GetResTotalAmount(station));
            _stationsModel.OnResAmountChanged += StationsModel_OnResAmountChanged;
        }

        private void StationsModel_OnResAmountChanged(string obj)
        {
            _view.UpdateResourceInfo(obj, _stationsModel.GetResTotalAmount(obj));
        }

        private void InitDronesCountControl()
        {
            _view.OnMaxDronesCountChanged += View_OnMaxDronesCountChanged;
        }

        private void View_OnMaxDronesCountChanged(float obj)
        {
            foreach (var station in _stationsModel.Stations)
                _stationsModel.SetMaxDronesCount(station, (int)obj);
        }
    }
}
