using System;
using UnityEngine;

namespace MineBlock3D.Scripts.Gameplay
{
    public class LoseTrigger : MonoBehaviour
    {
        public event Action callback;

        public void Construct(Action callback) => 
            this.callback = callback;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent<Block>(out var block))
                callback?.Invoke();
        }
    }
}