using UnityEngine;

namespace Game
{
    public class DroneData
    {
        public string DroneId;
        public string TargetId;
        public string StationId;

        public Vector3 Position;
        public DroneState State;
        public CargoData Cargo;
    }
}
