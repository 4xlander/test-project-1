using UnityEngine;

namespace Game
{
    public class SpaceResSpawner : ITickable
    {
        private SpaceResSpawnerConfig _config;
        private SpaceResModel _spaceResModel;
        private TickManager _tickManager;

        private Transform _spawnPoint;
        private float _spawnTimer = 0;

        public SpaceResSpawner(
            SpaceResSpawnerConfig config, SpaceResModel spaceResModel, TickManager tickManager)
        {
            _config = config;
            _spaceResModel = spaceResModel;

            _tickManager = tickManager;
            _tickManager.Register(this);
        }

        public void Init(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        public void Tick()
        {
            if (_spaceResModel.Resources.Count >= _config.MaxCount || _config.SpaceResources.Count < 1)
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

            var resourceView = Object.Instantiate(resourceConfig.Prefab, _spawnPoint);
            resourceView.transform.position = randomPosition;

            var resId = _spaceResModel.CreateRes(randomPosition, resourceConfig);
            new SpaceResController(resId, _spaceResModel, resourceView).Init(resourceConfig);
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
