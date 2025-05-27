using System;
using System.Collections.Generic;

namespace Game
{
    public class StationsModel
    {
        private readonly Dictionary<string, StationData> _stationsDataMap;

        public StationsModel()
        {
            _stationsDataMap = new Dictionary<string, StationData>();
        }

        /// <summary>
        /// Creates new station data
        /// </summary>
        /// <returns>Station Id</returns>
        public string AddStation()
        {
            var id = Guid.NewGuid().ToString();
            var data = new StationData
            {
                StationId = id,
            };

            _stationsDataMap.Add(id, data);

            return id;
        }

        public int GetMaxDronesCount(string stationId)
        {
            int result = 0;
            if (_stationsDataMap.TryGetValue(stationId, out var data))
                result = data.MaxDronesCount;

            return result;
        }

        public IReadOnlyList<string> GetDrones(string stationId)
        {
            if (_stationsDataMap.TryGetValue(stationId, out var data))
                return data.Drones;
            else
                return new List<string>();
        }

        public void AddDrone(string stationId, string droneId)
        {
            if (_stationsDataMap.ContainsKey(stationId))
                _stationsDataMap[stationId].Drones.Add(droneId);
        }

        public void RemoveDrone(string stationId, string obj)
        {
            if (_stationsDataMap.ContainsKey(stationId))
            {
                var droneList = _stationsDataMap[stationId].Drones;
                if (droneList.Contains(obj))
                    droneList.Remove(obj);
            }
        }
    }
}
