using UnityEngine;

namespace Game
{
    public class SpaceResSpawner : MonoBehaviour, ITickable
    {
        private SpaceResSpawnerConfig _config;
        private SpaceResModelService _modelService;
        private TickManager _tickManager;

        private float _spawnTimer = 0;

        private void Start()
        {
            _tickManager.Register(this);
        }

        private void OnDestroy()
        {
            _tickManager.Unregister(this);
        }

        public void Init(
            SpaceResSpawnerConfig config, SpaceResModelService modelService, TickManager tickManager)
        {
            _config = config;
            _modelService = modelService;
            _tickManager = tickManager;
        }

        public void Tick()
        {
            if (_modelService.Models.Count >= _config.MaxCount || _config.SpaceResources.Count < 1)
                return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0)
            {
                _spawnTimer = _config.SpawnInterval;
                SpawnRandomResource();
            }
        }

        private void SpawnRandomResource()
        {
            var resourceConfig = GetRandomConfig();
            var randomPosition = GetRandomPosition();

            var resourceView = Instantiate(resourceConfig.Prefab, transform);
            resourceView.transform.position = randomPosition;

            var resourceController = new SpaceResController(_modelService);
            resourceController.Init(resourceConfig, resourceView);
        }

        private SpaceResConfig GetRandomConfig()
        {
            return _config.SpaceResources[Random.Range(0, _config.SpaceResources.Count - 1)];
        }

        private Vector3 GetRandomPosition()
        {
            var randomPoint2D = Random.insideUnitCircle * _config.SpawnRadius;
            return new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        }
    }
}
