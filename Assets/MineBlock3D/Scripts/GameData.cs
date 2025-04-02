using MineBlock3D.Scripts.Service;
using UnityEngine;

namespace MineBlock3D.Scripts
{
    [CreateAssetMenu( fileName = "GameData", menuName = "Infrastructure/GameData" )]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public Assets Assets { get; private set; }
    }
}
