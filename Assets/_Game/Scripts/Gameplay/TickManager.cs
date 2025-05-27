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

        public void Tick()
        {
            foreach (var tickable in _tickables)
                tickable.Tick();
        }

        public void Register(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void Unregister(ITickable tickable)
        {
            if (_tickables.Contains(tickable))
                _tickables.Remove(tickable);
        }
    }
}
