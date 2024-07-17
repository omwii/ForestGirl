using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeypadKey : MonoBehaviour, IInteractible
{
    [SerializeField] private string _keyValue;
    [SerializeField] private UnityEvent _onInteract;

    public UnityEvent OnInteract { get { return _onInteract; } }
    public bool InteractValue { get; }
    public string KeyValue { get { return _keyValue; } }

    public void Interact()
    {
        OnInteract.Invoke();
    }
}
