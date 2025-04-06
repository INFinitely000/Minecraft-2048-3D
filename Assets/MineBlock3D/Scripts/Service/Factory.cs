using MineBlock3D.Scripts.Gameplay;
using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public class Factory : IFactory
    {
        public const string BlockAssetName = "Block";
        
        public readonly IAssets assets;
        
        public Factory(IAssets assets)
        {
            this.assets = assets;
        }

        public Block CreateBlock(int cost, Vector3 position, Quaternion rotation)
        {
            var createdBlock = Create<Block>(BlockAssetName, position, rotation);
            var blockInfo = assets.GetBlockInfo(cost);

            createdBlock.Construct(cost, blockInfo, this);

            return createdBlock;
        }

        public TComponent Create<TComponent>(string key) where TComponent : Component
        {
            var prefab = assets.Single<TComponent>(key);
            return Object.Instantiate(prefab);
        }
        
        public TComponent Create<TComponent>(string key, Vector3 position, Quaternion rotation) where TComponent : Component
        {
            var prefab = assets.Single<TComponent>(key);
            return Object.Instantiate(prefab, position, rotation);
        }
    }
}