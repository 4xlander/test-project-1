using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private SpaceResSpawnerConfig _spaceResSpawner;

        public SpaceResSpawnerConfig SpaceResSpawner => _spaceResSpawner;
    }
}
