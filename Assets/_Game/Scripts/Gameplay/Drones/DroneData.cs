using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DroneData
    {
        public string DroneId;
        public string TargetId;
        public string StationId;

        public float Speed;
        public bool PathVisibility;
        public Vector3 Position;
        public DroneState State;
        public List<CargoData> Cargo = new();
    }
}
