using System;
using MineBlock3D.Scripts.Gameplay;
using ProjectTools;
using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    [CreateAssetMenu(fileName = "Assets", menuName = "Infrastructure/Assets")]
    public class Assets : ScriptableObject, IAssets
    {
        [SerializeField] private SerializableDictionary<string, Component> _prefabs;
        [SerializeField] private BlockInfo[] _blockInfos;

        public TComponent Single<TComponent>(string key) where TComponent : Component =>
            _prefabs[key].GetComponent<TComponent>();

        public BlockInfo GetBlockInfo(int cost)
        {
            if (cost < 0 || cost >= _blockInfos.Length)
                throw new ArgumentOutOfRangeException(nameof(cost));
            
            return _blockInfos[cost];
        }
    }
}