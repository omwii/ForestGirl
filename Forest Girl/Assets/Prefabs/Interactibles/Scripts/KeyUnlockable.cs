using Player;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class KeyUnlockable : MonoBehaviour
{
    [SerializeField] private string _keyId;
    [SerializeField] private UnityEvent _onUnlock;

    private InventoryPerformer _inventoryPerformer;

    private void Start()
    {
        _inventoryPerformer = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .InventoryPerformer;
    }

    public void TryUnlock()
    {
        if (_inventoryPerformer.CurrentInventory[_inventoryPerformer.CurrentSlot]?.Name == _keyId)
        {
            _inventoryPerformer.InventorySlotsIcons[_inventoryPerformer.CurrentSlot].sprite = _inventoryPerformer.TransparentIcon;
            _inventoryPerformer.CurrentInventory[_inventoryPerformer.CurrentSlot] = null;
            _onUnlock.Invoke();
        }
    }
}
