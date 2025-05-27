using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StationData
    {
        public string StationId;
        public Vector3 Position;
        public int MaxDronesCount = 3;
        public List<string> Drones = new();
        public List<CargoData> Resources = new();
    }
}
