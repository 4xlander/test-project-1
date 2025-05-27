using UnityEngine;

namespace Game
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [Space]
        [SerializeField] private Transform _spaceResSpawnPoint;
        [SerializeField] private StationView[] _stationViews;

        private TickManager _tickManager;

        private void Awake()
        {
            _tickManager = new TickManager();

            var spaceResModel = new SpaceResModel();
            new SpaceResSpawner(
                _gameConfig.SpaceResSpawner, spaceResModel, _tickManager).Init(_spaceResSpawnPoint);

            var dronesModel = new DronesModel();

            var droneFactory = new DroneFactory(
                _gameConfig.StationConfig.Drones, dronesModel, spaceResModel, _tickManager);

            var stationsModel = new StationsModel();
            foreach (var view in _stationViews)
            {
                var stationId = stationsModel.AddStation();
                var controller = new StationController(
                    stationId, stationsModel, view, dronesModel, droneFactory, _tickManager);
            }
        }

        private void Update()
        {
            _tickManager.Tick();
        }
    }
}
