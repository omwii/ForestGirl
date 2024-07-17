using UnityEngine;

public class InteractBaseEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;

    public void InteractEffects()
    {
        _audioSource.Play();
        _animator.SetBool("Interact", true);
    }
}
