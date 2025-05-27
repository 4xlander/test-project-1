using System.Collections.Generic;

namespace Game
{
    public class StationData
    {
        public string StationId;
        public int MaxDronesCount = 1;
        public List<string> Drones = new();
        public List<CargoData> Resources = new();
    }
}
