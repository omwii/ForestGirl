using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class InteractionPerformer
    {
        public enum InteractType
        {
            Interact,
            Grab
        }
        private Transform _playerCamera;
        private float _interactDistance;
        private Image _cursorImage;
        private Sprite _defaultCursorSprite;
        private Sprite _interactCursorSprite;
        private Sprite _grabCursorSprite;

        //Public Methods
        public void CursorSpriteChange() //Should be updated each frame
        {
            Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObject))
                {
                    _cursorImage.sprite = _interactCursorSprite;
                }
                else if (hitInfo.collider.gameObject.TryGetComponent(out IGrabbable grabObject))
                {
                    _cursorImage.sprite = _grabCursorSprite;
                }
                else
                {
                    _cursorImage.sprite = _defaultCursorSprite;
                }
            }
            else
            {
                _cursorImage.sprite = _defaultCursorSprite;
            }
        }

        public void Interact() //Only on pressed interact button
        {
            Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractible interactObject))
                {
                    interactObject.Interact();
                }
                else if (hitInfo.collider.gameObject.TryGetComponent(out IGrabbable grabObject))
                {
                    grabObject.Grab();
                }
            }
        }

        //Constructor
        public InteractionPerformer(Transform playerCamera,
            float interactDistance,
            Image cursorImage,
            Sprite defaultCursorSprite,
            Sprite interactCursorSprite,
            Sprite grabCursorSprite)
        {
            _playerCamera = playerCamera;
            _interactDistance = interactDistance;
            _cursorImage = cursorImage;
            _defaultCursorSprite = defaultCursorSprite;
            _interactCursorSprite = interactCursorSprite;
            _grabCursorSprite = grabCursorSprite;
        }
    }
}