using UnityEngine;

namespace Player
{
    public class AnimationPerformer
    {
        private Animator _playerAnimator;

        //Public methods
        public void UpdateAnimationParameters(float strafeSpeed, float forwardSpeed, bool isCrouching)
        {
            _playerAnimator.SetFloat("Forward", forwardSpeed);
            _playerAnimator.SetFloat("Strafe", strafeSpeed);
            _playerAnimator.SetBool("Crouching", isCrouching);
        }

        public void SetMovingState(bool state)
        {
            _playerAnimator.SetBool("Moving", state);
        }

        public void Jump()
        {
            _playerAnimator.CrossFade("Jump", 0.2f);
        }

        //Constructor
        public AnimationPerformer(Animator playerAnimator)
        {
            _playerAnimator = playerAnimator;
        }
    }
}