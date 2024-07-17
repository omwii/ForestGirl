using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ListenPerformer
{
    private Item[] _soundItems;
    private Item _currentFallenItem;
    public Vector3 CurrentItemPosition { get { return _currentFallenItem.transform.position; } private set { } }
    public UnityEvent OnCollisionEvent = new UnityEvent();

    private void OnCollision(Item soundItem)
    {
        _currentFallenItem = soundItem;
        CurrentItemPosition = soundItem.transform.position;
        OnCollisionEvent.Invoke();
    }

    //Constructor
    public ListenPerformer(Item[] soundItems)
    {
        _soundItems = soundItems;

        foreach (Item item in _soundItems) 
        {
            item.OnCollision.AddListener( delegate { OnCollision(item); } );
        }
    }
}
