using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StationsModel
    {
        private readonly Dictionary<string, StationData> _dataMap;

        public StationsModel()
        {
            _dataMap = new Dictionary<string, StationData>();
        }

        /// <summary>
        /// Creates new station data
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Station Id</returns>
        public string AddStation(Vector3 position)
        {
            var id = Guid.NewGuid().ToString();
            var data = new StationData
            {
                StationId = id,
                Position = position,
            };

            _dataMap.Add(id, data);

            return id;
        }

        public int GetMaxDronesCount(string stationId)
        {
            int result = 0;
            if (_dataMap.TryGetValue(stationId, out var data))
                result = data.MaxDronesCount;

            return result;
        }

        public IReadOnlyList<string> GetDrones(string stationId)
        {
            if (_dataMap.TryGetValue(stationId, out var data))
                return data.Drones;
            else
                return new List<string>();
        }

        public void AddDrone(string stationId, string droneId)
        {
            if (_dataMap.ContainsKey(stationId))
                _dataMap[stationId].Drones.Add(droneId);
        }

        public void RemoveDrone(string stationId, string obj)
        {
            if (_dataMap.ContainsKey(stationId))
            {
                var droneList = _dataMap[stationId].Drones;
                if (droneList.Contains(obj))
                    droneList.Remove(obj);
            }
        }

        public Vector3 GetPosition(string stationId)
        {
            return _dataMap[stationId].Position;
        }
    }
}
