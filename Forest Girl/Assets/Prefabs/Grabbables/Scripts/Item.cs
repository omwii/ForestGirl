using Player;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IGrabbable
{
    [SerializeField] private string _idName;
    [SerializeField] private Sprite _inventoryIcon;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private UnityEvent _onGrab;
    [SerializeField] private UnityEvent _onCollision;

    //Public Fields
    public string Name { get { return _idName; } }
    public Sprite Icon { get { return _inventoryIcon; } }
    public GameObject Prefab { get { return _itemPrefab; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }
    public UnityEvent OnCollision { get { return _onCollision; } }

    //Private Fields
    private InventoryPerformer _inventoryPerformer;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _inventoryPerformer = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .InventoryPerformer;
    }

    public void Grab()
    {
        _inventoryPerformer.GrabItem(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision.Invoke();
    }
}
