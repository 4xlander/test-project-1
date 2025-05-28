using UnityEngine;

namespace Game
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private UIService _uiService;
        [Space]
        [SerializeField] private Transform _spaceResSpawnPoint;
        [SerializeField] private StationView[] _stationViews;

        private TickManager _tickManager;

        private void Awake()
        {
            _tickManager = new TickManager();

            var spaceResModel = new SpaceResModel();

            var spaceResSpawnModel = new SpaceResSpawnerModel();
            new SpaceResSpawner(
                spaceResSpawnModel, _gameConfig.SpaceResSpawner, spaceResModel, _tickManager).Init(_spaceResSpawnPoint);

            var dronesModel = new DronesModel();

            var stationsModel = new StationsModel();

            var droneFactory = new DroneFactory(
                _gameConfig.StationConfig.Drones, dronesModel, spaceResModel, stationsModel, _tickManager);

            foreach (var view in _stationViews)
            {
                var stationId = stationsModel.AddStation(view.transform.position);
                var controller = new StationController(
                    stationId, stationsModel, view, dronesModel, droneFactory, _tickManager);
            }

            new GameplayUIController(
                _uiService.GameplayUIView, stationsModel, dronesModel, spaceResSpawnModel).Init();
        }

        private void Update()
        {
            _tickManager.Tick();
        }
    }
}
