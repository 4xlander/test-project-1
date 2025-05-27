using UnityEngine;

namespace Game
{
    public class StationView : MonoBehaviour
    {
        [SerializeField] private Transform _droneSpawnPoin;

        public Transform DroneSpawnPoint => _droneSpawnPoin;
    }
}

