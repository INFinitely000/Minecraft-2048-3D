using UnityEngine;

namespace MineBlock3D.Scripts.Gameplay
{
    public class Game : MonoBehaviour
    {
        public static bool IsActive { get; private set; }
        
        public static void Launch()
        {
            foreach (var loseTrigger in Object.FindObjectsByType<LoseTrigger>(FindObjectsSortMode.None))
                loseTrigger.callback += Lose;

            IsActive = true;
        }
        
        public static void Lose()
        {
            foreach (var loseTrigger in Object.FindObjectsByType<LoseTrigger>(FindObjectsSortMode.None))
                loseTrigger.callback -= Lose;
            
            var blocks = Object.FindObjectsByType<Block>(FindObjectsSortMode.None);

            foreach (var block in blocks)
                block.Rigidbody.isKinematic = true;

            Object.FindFirstObjectByType<GameCamera>().PlayLoseAnimation();

            IsActive = false;
        }
    }
}