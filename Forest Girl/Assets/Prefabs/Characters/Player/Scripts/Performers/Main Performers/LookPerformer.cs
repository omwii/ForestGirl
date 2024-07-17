using UnityEngine;

namespace Player
{
    public class LookPerformer
    {
        private Transform _playerTransform;
        private Transform _cameraTransform;
        private float _sensetivity;

        private float xRotation = 0f;

        //Public methods
        public void Look(Vector2 lookDirection)
        {
            Vector2 mouseInput = lookDirection * Time.deltaTime * _sensetivity;

            //Player Rotating
            _playerTransform.Rotate(Vector3.up, mouseInput.x);

            //Camera Rotating
            xRotation -= mouseInput.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90);
            Vector3 targetRotation = _cameraTransform.eulerAngles;
            targetRotation.x = xRotation;
            _cameraTransform.eulerAngles = targetRotation;
        }

        //Constructor
        public LookPerformer(Transform playerTransform,
            Transform cameraTransform,
            float sensitivity)
        {
            _playerTransform = playerTransform;
            _cameraTransform = cameraTransform;
            _sensetivity = sensitivity;
        }
    }
}