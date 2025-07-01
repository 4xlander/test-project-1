using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class DronesModel
    {
        public event Action<string> OnDroneRemoved;
        public event Action<string> OnSpeedChanged;
        public event Action<string> OnTargetChanged;
        public event Action OnPathVisibilityChanged;

        private readonly Dictionary<string, DroneData> _dataMap;
        private bool _pathVisibility = false;

        public DronesModel()
        {
            _dataMap = new Dictionary<string, DroneData>();
        }

        public IReadOnlyCollection<string> Drones =>
            _dataMap.Keys;

        public string AddDrone(string stationId)
        {
            var droneId = Guid.NewGuid().ToString();
            var data = new DroneData
            {
                DroneId = droneId,
                StationId = stationId,
            };

            _dataMap.Add(droneId, data);

            return droneId;
        }

        public void RemoveDrone(string droneId)
        {
            if (!_dataMap.ContainsKey(droneId))
                return;

            _dataMap.Remove(droneId);
            OnDroneRemoved?.Invoke(droneId);
        }

        public DroneState GetState(string droneId)
        {
            if (_dataMap.TryGetValue(droneId, out var data))
                return data.State;
            else
                return DroneState.Idle;
        }

        public void ChangeState(string droneId, DroneState droneState)
        {
            if (!_dataMap.TryGetValue(droneId, out var data))
                return;

            data.State = droneState;
        }

        public void SetTargetResource(string droneId, string resId)
        {
            if (!_dataMap.TryGetValue(droneId, out var data))
                return;

            data.TargetId = resId;
            OnTargetChanged?.Invoke(droneId);
        }

        public string GetTargetResource(string droneId)
        {
            return _dataMap[droneId].TargetId;
        }

        public void AddCargo(string _dronId, SpaceResType resType, float amount)
        {
            if (!_dataMap.TryGetValue(_dronId, out var data))
                return;

            var stored = data.Cargo.FirstOrDefault(c => c.ResType == resType);
            if (stored != null)
                stored.Amount += amount;
            else
                data.Cargo.Add(new CargoData
                {
                    ResType = resType,
                    Amount = amount,
                });
        }

        public string GetStationId(string droneId)
        {
            return _dataMap[droneId].StationId;
        }

        public IReadOnlyList<CargoData> GetCargo(string droneId)
        {
            return _dataMap[droneId].Cargo;
        }

        public void ClearCargo(string droneId)
        {
            if (_dataMap.TryGetValue(droneId, out var data))
                data.Cargo.Clear();
        }

        public float GetSpeed(string droneId)
        {
            return _dataMap[droneId].Speed;
        }

        public void SetSpeed(string droneId, float value)
        {
            if (!_dataMap.TryGetValue(droneId, out var data))
                return;

            data.Speed = value;
            OnSpeedChanged?.Invoke(droneId);
        }

        public bool GetPathVisibility() => _pathVisibility;

        public void SetPathVisibility(bool value)
        {
            _pathVisibility = value;
            OnPathVisibilityChanged?.Invoke();
        }

        public void SetPosition(string droneId, Vector3 position)
        {
            if (!_dataMap.TryGetValue(droneId, out var data))
                return;

            data.Position = position;
        }

        public Vector3 GetPosition(string droneId)
        {
            return _dataMap[droneId].Position;
        }
    }
}