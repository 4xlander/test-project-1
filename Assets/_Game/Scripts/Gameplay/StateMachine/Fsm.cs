using System;
using System.Collections.Generic;

namespace Game
{
    public class Fsm
    {
        private readonly Dictionary<Type, IFsmState> _stateMap = new();
        private IFsmState _currentState;

        public void AddState<T>(T state) where T : IFsmState
        {
            _stateMap[state.GetType()] = state;
        }

        public void SetState<T>() where T : IFsmState
        {
            if (_currentState != null && _currentState.GetType() == typeof(T))
                return;

            if (_stateMap.TryGetValue(typeof(T), out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
