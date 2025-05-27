using System.Collections.Generic;

namespace Game
{
    public interface ITickable
    {
        void Tick();
    }

    public class TickManager
    {
        private readonly List<ITickable> _tickables = new();
        private readonly List<ITickable> _ticksToRegister = new();
        private readonly List<ITickable> _ticksToUnregister = new();

        public void Tick()
        {
            RegisterTickables();
            UnregisterTickables();

            foreach (var tickable in _tickables)
                tickable.Tick();
        }

        public void Register(object obj)
        {
            if (obj is ITickable tickable)
                _ticksToRegister.Add(tickable);
        }

        public void Unregister(object obj)
        {
            if (obj is ITickable tickable)
                _ticksToUnregister.Add(tickable);
        }

        private void RegisterTickables()
        {
            foreach (var item in _ticksToRegister)
                if (!_tickables.Contains(item))
                    _tickables.Add(item);

            _ticksToRegister.Clear();
        }

        private void UnregisterTickables()
        {
            foreach (var item in _ticksToUnregister)
                if (_tickables.Contains(item))
                    _tickables.Remove(item);

            _ticksToUnregister.Clear();
        }
    }
}
