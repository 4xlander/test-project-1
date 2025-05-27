using System;
using System.Collections.Generic;

namespace Game
{
    public class DronesModel
    {
        public event Action<string> OnDroneRemoved;

        private readonly Dictionary<string, DroneData> _dronesDataMap;

        public DronesModel()
        {
            _dronesDataMap = new Dictionary<string, DroneData>();
        }

        public string AddDrone(string stationId)
        {
            var droneId = Guid.NewGuid().ToString();
            var data = new DroneData
            {
                DroneId = droneId,
                StationId = stationId,
            };

            _dronesDataMap.Add(droneId, data);

            return droneId;
        }

        public void RemoveDrone(string droneId)
        {
            if (!_dronesDataMap.ContainsKey(droneId))
                return;

            _dronesDataMap.Remove(droneId);
            OnDroneRemoved?.Invoke(droneId);
        }

        public string GetDroneTarget(string droneId)
        {
            if (_dronesDataMap.TryGetValue(droneId, out var data))
                return data.TargetId;
            else
                return string.Empty;
        }
    }
}