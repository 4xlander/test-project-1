using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DroneConfig", menuName = "Config/DroneConfig")]
    public class DroneConfig : ScriptableObject
    {
        [SerializeField] private DroneType _droneType;
        [SerializeField] private float _moveSpeedBase = 1f;

        [Tooltip("Space resource amount per second")]
        [SerializeField] private float _gatherSpeedBase;

        [Space]
        [SerializeField] private DroneView _prefab;


        public DroneType DroneType => _droneType;
        public float MoveSpeedBase => _moveSpeedBase;
        public float GatherSpeedBase => _gatherSpeedBase;
        public DroneView Prefab => _prefab;

        private void OnValidate()
        {
            if (_moveSpeedBase < 1f)
                _moveSpeedBase = 1f;
        }
    }
}
