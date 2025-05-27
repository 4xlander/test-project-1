using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SpaceResConfig", menuName = "Config/SpaceResConfig")]
    public class SpaceResConfig : ScriptableObject
    {
        [SerializeField] private SpaceResType _resource;
        [SerializeField] private float _amount = 100;
        [Space]
        [SerializeField] private SpaceResView _prefab;

        public SpaceResType Type => _resource;
        public float Amount => _amount;
        public SpaceResView Prefab => _prefab;
    }
}
