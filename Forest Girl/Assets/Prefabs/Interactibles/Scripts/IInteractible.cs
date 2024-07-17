using UnityEngine.Events;

public interface IInteractible
{
    public void Interact();
    public UnityEvent OnInteract { get; }
    public bool InteractValue { get; }
}
