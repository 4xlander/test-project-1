using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "StationConfig", menuName = "Config/StationConfig")]
    public class StationConfig : ScriptableObject
    {
        [SerializeField] private DroneConfig[] _drones;

        public IReadOnlyList<DroneConfig> Drones => _drones;
    }
}
