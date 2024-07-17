using UnityEngine;

namespace Player
{
    public class GravityPerformer
    {
        private PlayerController _playerController;
        private CharacterController _characterController;
        private float _gravityForce;

        //Public Methods
        public void ApplyGravity()
        {
            if (_characterController.isGrounded && _playerController.VerticalVelocity < 0.0f)
            {
                _playerController.VerticalVelocity = -1.0f;
            }
            else
            {
                _playerController.VerticalVelocity += _gravityForce * Time.deltaTime;
            }

            _characterController.Move(new Vector3(0, _playerController.VerticalVelocity * Time.deltaTime, 0));
        }

        //Constructor
        public GravityPerformer(PlayerController playerController,
            CharacterController characterController,
            float gravityForce)
        {
            _playerController = playerController;
            _characterController = characterController;
            _gravityForce = gravityForce;
        }
    }
}