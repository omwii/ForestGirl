using UnityEngine;
using DG.Tweening;

namespace Player
{
    public class CrouchPerformer
    {
        private CharacterController _characterController;
        private Transform _playerTransform;
        private int _playerLayerMask;
        private float _targetHeight;
        private float _startHeight;
        private float _crouchSmoothTime;

        //Public Methods
        public void Crouch()
        {
            DOTween.To(() => _characterController.height, x => _characterController.height = x, _targetHeight, _crouchSmoothTime);
        }

        public void UnCrouch()
        {
            DOTween.To(() => _characterController.height, x => _characterController.height = x, _startHeight, _crouchSmoothTime);
        }

        public bool CheckIsFreeAbove()
        {
            return !Physics.Raycast(_playerTransform.position, Vector3.up, 1f, ~_playerLayerMask);
        }

        //Crouch
        public CrouchPerformer(Transform playerTransform,
            CharacterController characterController,
            int playerLayerMask,
            float targetHeight,
            float crouchSmoothTime)
        {
            _playerTransform = playerTransform;
            _characterController = characterController;
            _playerLayerMask = playerLayerMask;
            _targetHeight = targetHeight;
            _crouchSmoothTime = crouchSmoothTime;
            _startHeight = _characterController.height;
        }
    }
}