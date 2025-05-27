using UnityEngine;

namespace Game
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [Space]
        [SerializeField] private SpaceResSpawner _spaceResSpawner;

        private TickManager _tickManager;

        private void Awake()
        {
            _tickManager = new TickManager();

            var spaceResModelService = new SpaceResModelService();
            _spaceResSpawner.Init(_gameConfig.SpaceResSpawner, spaceResModelService, _tickManager);

            var gatheringTest = new SpaceResGatheringTest(spaceResModelService);
            _tickManager.Register(gatheringTest);
        }

        private void Update()
        {
            _tickManager.Tick();
        }
    }
}
