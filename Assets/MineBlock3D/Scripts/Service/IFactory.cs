using MineBlock3D.Scripts.Gameplay;
using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    public interface IFactory : IService
    {
        public Block CreateBlock(int cost, Vector3 position, Quaternion identity);
        
        public TComponent Create<TComponent>(string key) where TComponent : Component;
        public TComponent Create<TComponent>(string key, Vector3 position, Quaternion rotation) where TComponent : Component;
    }
}