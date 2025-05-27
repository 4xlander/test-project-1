using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private SpaceResSpawnerConfig _spaceResSpawner;
        [SerializeField] private StationConfig _stationConfig;

        public SpaceResSpawnerConfig SpaceResSpawner => _spaceResSpawner;
        public StationConfig StationConfig => _stationConfig;
    }
}
