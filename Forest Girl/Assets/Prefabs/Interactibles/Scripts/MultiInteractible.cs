using UnityEngine.Events;
using System.Linq;
using UnityEngine;

public class MultiInteractible : MonoBehaviour
{
    [SerializeField] private IInteractible[] _interactibleMembers;
    [SerializeField] private UnityEvent _onAllTrue;

    private bool[] _interactibleVariables;

    private void Start()
    {
        _interactibleVariables = new bool[_interactibleMembers.Length];

        foreach (var member in _interactibleMembers)
        {
            member.OnInteract.AddListener(CheckIfAllTrue);
        }
    }

    private void CheckIfAllTrue()
    {
        for (int i = 0;  i < _interactibleMembers.Length; i++) 
        {
            _interactibleVariables[i] = _interactibleMembers[i].InteractValue;
        }
        if (_interactibleVariables.All(x => x))
        {
            _onAllTrue.Invoke();
        }
    }
}