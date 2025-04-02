namespace MineBlock3D.Scripts.States
{
    public interface IExitableState
    {
        public void Exit();
    }

    public interface IState : IExitableState
    {
        public void Entry();
    }

    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Entry(TPayload payload);
    }
}