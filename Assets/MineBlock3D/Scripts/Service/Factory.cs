using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public class Factory : IFactory
    {
        public readonly IAssets Assets;
        
        public Factory(IAssets assets)
        {
            Assets = assets;
        }
        
        public TComponent Create<TComponent>(string key) where TComponent : Component
        {
            var prefab = Assets.Single<TComponent>(key);
            return Object.Instantiate(prefab);
        }
    }
}