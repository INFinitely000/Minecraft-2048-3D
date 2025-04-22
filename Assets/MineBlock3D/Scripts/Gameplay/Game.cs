using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace MineBlock3D.Scripts.Gameplay
{
    public class Game : MonoBehaviour
    {
        public static bool IsActive { get; private set; }
        public static int Points { get; private set; }

        public static event Action PointsAdded;
        
        
        public static void Launch()
        {
            foreach (var loseTrigger in Object.FindObjectsByType<LoseTrigger>(FindObjectsSortMode.None))
                loseTrigger.callback += Lose;

            Block.Concatted += OnBlockConcatted;

            Points = 0;
            IsActive = true;
        }

        public static void OnBlockConcatted(Block block)
        {
            Points += (int)Mathf.Pow(2, block.Cost) / 2;

            PointsAdded?.Invoke();
        }

        public static void Stop()
        {
            foreach (var loseTrigger in Object.FindObjectsByType<LoseTrigger>(FindObjectsSortMode.None))
                loseTrigger.callback -= Lose;

            Block.Concatted -= OnBlockConcatted;
                
            IsActive = false;
        }
        
        public static void Lose()
        {
            Stop();
            
            var blocks = Object.FindObjectsByType<Block>(FindObjectsSortMode.None);

            foreach (var block in blocks)
                block.Rigidbody.isKinematic = true;

            Object.FindFirstObjectByType<GameCamera>().PlayLoseAnimation();

        }



        public static void Blow()
        {
            var blocks = Object.FindObjectsByType<Block>(FindObjectsSortMode.None);

            foreach (var block in blocks)
                block.Rigidbody.AddForce(new Vector3(0f, 0f, 5f), ForceMode.VelocityChange);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        }
    }
}