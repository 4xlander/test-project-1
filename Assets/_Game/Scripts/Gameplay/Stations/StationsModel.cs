using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class StationsModel
    {
        public event Action<string> OnResAmountChanged;

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

        public void TransferCargo(string stationId, CargoData item)
        {
            if (!_dataMap.ContainsKey(stationId))
                return;

            var resources = _dataMap[stationId].Resources;
            var res = resources.FirstOrDefault(r => r.ResType == item.ResType);
            if (res != null)
                res.Amount += item.Amount;
            else
                resources.Add(new CargoData
                {
                    ResType = item.ResType,
                    Amount = item.Amount,
                });

            OnResAmountChanged?.Invoke(stationId);
        }

        public float GetResTotalAmount(string stationId)
        {
            var result = 0f;
            if (_dataMap.ContainsKey(stationId))
                foreach (var item in _dataMap[stationId].Resources)
                    result += item.Amount;

            return result;
        }
    }
}
