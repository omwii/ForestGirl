using Player;
using UnityEngine;
using UnityEngine.Events;

public class Locker : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private Transform _hidePoint;
    [SerializeField] private Transform _unHidePoint;
    [SerializeField] private UnityEvent _onInteract;

    public UnityEvent OnInteract { get { return _onInteract; } }
    public bool InteractValue { get; set; } = false;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }

    public void Interact()
    {
        _onInteract.Invoke();

        if (!InteractValue)
        {
            InteractValue = true;
            _playerController.DummyIn(_lookPoint, _hidePoint);
        }
        else
        {
            InteractValue = false;
            _playerController.DummyOut(_unHidePoint);
        }
    }
}