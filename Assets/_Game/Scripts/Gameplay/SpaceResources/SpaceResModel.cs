using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SpaceResModel
    {
        public event Action<string> OnStateChanged;
        public event Action<string> OnAmountChanged;
        public event Action<string> OnResRemoved;

        private readonly Dictionary<string, SpaceResData> _dataMap;

        public SpaceResModel()
        {
            _dataMap = new Dictionary<string, SpaceResData>();
        }

        public bool Contains(string resId) =>
            _dataMap.ContainsKey(resId);

        public IReadOnlyCollection<string> Resources =>
            _dataMap.Keys;

        public string CreateRes(Vector3 position, SpaceResConfig config)
        {
            var id = Guid.NewGuid().ToString();
            var data = new SpaceResData
            {
                ResId = id,
                Type = config.Type,
                Amount = config.Amount,
                Position = position,
            };

            _dataMap[id] = data;
            return id;
        }

        public void RemoveRes(string resId)
        {
            if (!_dataMap.ContainsKey(resId))
                return;

            _dataMap.Remove(resId);
            OnResRemoved?.Invoke(resId);
        }

        public bool IsFree(string resId)
        {
            return _dataMap[resId].State == SpaceResState.Idle;
        }

        public void Reserve(string resId)
        {
            if (_dataMap.TryGetValue(resId, out var data))
                data.State = SpaceResState.Target;
        }

        public void Unreserve(string resId)
        {
            if (_dataMap.TryGetValue(resId, out var data))
                data.State = SpaceResState.Idle;
        }

        public SpaceResType GetResType(string resId)
        {
            return _dataMap[resId].Type;
        }

        public float GetAmount(string resId)
        {
            if (_dataMap.ContainsKey(resId))
                return _dataMap[resId].Amount;
            else
                return 0f;
        }

        public float Gather(string resId, float value)
        {
            if (!_dataMap.TryGetValue(resId, out var data))
                return 0f;

            value = Mathf.Clamp(value, 0, data.Amount);
            data.Amount -= value;

            OnAmountChanged?.Invoke(resId);
            return value;
        }

        public Vector3 GetPosition(string resId)
        {
            return _dataMap[resId].Position;
        }
    }
}
