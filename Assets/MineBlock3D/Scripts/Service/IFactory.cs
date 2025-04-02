using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public interface IFactory : IService
    {
        public TComponent Create<TComponent>(string key) where TComponent : Component;
    }
}