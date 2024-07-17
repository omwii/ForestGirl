using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBaseAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAnim()
    {
        _animator.Play("Open");
    }
}
