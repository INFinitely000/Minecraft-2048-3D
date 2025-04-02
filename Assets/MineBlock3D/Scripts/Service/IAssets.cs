using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public interface IAssets : IService
    {
        public TComponent Single<TComponent>(string key) where TComponent : Component;
    }
}