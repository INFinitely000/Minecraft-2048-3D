namespace MineBlock3D.Scripts.Service
{
    public interface IInput : IService
    {
        public InputState Fire { get; }
        public float Horizontal { get; }
    }
}