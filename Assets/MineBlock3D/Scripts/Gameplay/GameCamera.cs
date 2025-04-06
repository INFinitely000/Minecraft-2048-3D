using UnityEngine;

namespace MineBlock3D.Scripts.Gameplay
{
    public class GameCamera : MonoBehaviour
    {
        public const string LoseAnimationName = "Lose";
        
        [SerializeField] private Animator _animator;

        public void PlayLoseAnimation() =>
            _animator.SetTrigger(LoseAnimationName);
    }
}