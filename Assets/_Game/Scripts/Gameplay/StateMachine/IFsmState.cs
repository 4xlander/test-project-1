namespace Game
{
    public interface IFsmState
    {
        void Exit();
        void Enter();
        void Update();
    }
}
