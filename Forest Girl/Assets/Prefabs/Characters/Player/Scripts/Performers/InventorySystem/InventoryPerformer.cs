using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class InventoryPerformer
    {
        //Dependencies
        private Image[] _inventorySlotsIcons;
        private Transform _cameraTransform;
        private Sprite _transparentIcon;

        private int _currentSlot = 0;
        private IGrabbable[] _currentInventory = new IGrabbable[5];

        public int CurrentSlot { get { return _currentSlot; } }
        public IGrabbable[] CurrentInventory { get { return _currentInventory; } }
        public Image[] InventorySlotsIcons { get { return _inventorySlotsIcons; } }
        public Sprite TransparentIcon { get { return _transparentIcon; } }

        //Public Methods
        public void GrabItem(IGrabbable inventoryItem)
        {
            if (_currentInventory[_currentSlot] != null)
                return;
            inventoryItem.gameObject.SetActive(false);
            _inventorySlotsIcons[_currentSlot].sprite = inventoryItem.Icon;
            _currentInventory[_currentSlot] = inventoryItem;
        }

        public void ThrowItem()
        {
            if (_currentInventory[_currentSlot] == null)
                return;
            _currentInventory[_currentSlot].gameObject.transform.parent = null;
            _currentInventory[_currentSlot].gameObject.transform.position = _cameraTransform.position + _cameraTransform.forward;
            _currentInventory[_currentSlot].gameObject.transform.rotation = _cameraTransform.rotation;
            _currentInventory[_currentSlot].gameObject.SetActive(true);
            _currentInventory[_currentSlot].Rigidbody.AddForce(_currentInventory[_currentSlot].gameObject.transform.forward, ForceMode.Impulse);
            _inventorySlotsIcons[_currentSlot].sprite = _transparentIcon;
            _currentInventory[_currentSlot] = null;
        }

        public void ChangeCurrentSlot(int targetSlot)
        {
            _currentSlot = targetSlot;
        }

        //Constructor
        public InventoryPerformer(Image[] inventorySlotsIcons,
            Transform cameraTransform,
            Sprite transparentIcon)
        {
            _inventorySlotsIcons = inventorySlotsIcons;
            _cameraTransform = cameraTransform;
            _transparentIcon = transparentIcon;
        }
    }
}