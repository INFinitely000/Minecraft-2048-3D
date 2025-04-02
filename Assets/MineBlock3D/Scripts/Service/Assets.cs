using ProjectTools;
using UnityEngine;

namespace MineBlock3D.Scripts.Service
{
    [CreateAssetMenu(fileName = "Assets", menuName = "Infrastructure/Assets")]
    public class Assets : ScriptableObject, IAssets
    {
        [SerializeField] private SerializableDictionary<string, Component> _prefabs;

        public TComponent Single<TComponent>(string key) where TComponent : Component =>
            _prefabs[key].GetComponent<TComponent>();
    }
}