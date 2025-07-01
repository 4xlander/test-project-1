namespace Game
{
    public abstract class FsmState : IFsmState
    {
        protected Fsm _fsm;

        public FsmState(Fsm fsm)
        {
            _fsm = fsm;
        }

        public virtual void Exit() { }
        public virtual void Enter() { }
        public virtual void Update() { }
    }
}
