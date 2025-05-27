using System;
using UnityEngine;

namespace Game
{
    public class SpaceResModel
    {
        public event Action OnStateChanged;
        public event Action OnAmountChanged;

        private readonly SpaceResData _data;

        public SpaceResModel(SpaceResData data)
        {
            _data = data;
        }

        public SpaceRes Type => _data.Type;

        public SpaceResState State
        {
            get => _data.State;
            private set
            {
                if (_data.State == value)
                    return;
                _data.State = value;

                OnStateChanged?.Invoke();
            }
        }

        public Vector3 Position
        {
            get => _data.Position;
            private set
            {
                if (_data.Position == value)
                    return;
                _data.Position = value;
            }
        }

        public float Amount
        {
            get => _data.Amount;
            private set
            {
                if (_data.Amount == value)
                    return;
                _data.Amount = value;

                OnAmountChanged?.Invoke();
            }
        }

        public float Gather(float value)
        {
            value = Mathf.Clamp(value, 0, Amount);
            Amount -= value;
            return value;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetState(SpaceResState newState)
        {
            State = newState;
        }
    }
}
