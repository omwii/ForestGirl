using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent _onInteract;

    public UnityEvent OnInteract { get { return _onInteract; } }

    public bool InteractValue { get; }

    public void Interact()
    {
        _onInteract.Invoke();
    }
}
