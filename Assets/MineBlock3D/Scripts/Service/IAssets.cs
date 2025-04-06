using MineBlock3D.Scripts.Gameplay;
using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public interface IAssets : IService
    {
        public TComponent Single<TComponent>(string key) where TComponent : Component;

        public BlockInfo GetBlockInfo(int blockCost);
    }
}