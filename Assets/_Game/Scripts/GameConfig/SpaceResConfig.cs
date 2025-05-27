using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SpaceResConfig", menuName = "Config/SpaceResConfig")]
    public class SpaceResConfig : ScriptableObject
    {
        [SerializeField] private SpaceRes _resource;
        [SerializeField] private float _amount = 100;
        [Space]
        [SerializeField] private SpaceResView _prefab;

        public SpaceRes Resource => _resource;
        public float Amount => _amount;
        public SpaceResView Prefab => _prefab;
    }
}
