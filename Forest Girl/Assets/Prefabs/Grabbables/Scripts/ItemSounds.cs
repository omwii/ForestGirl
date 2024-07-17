using UnityEngine;

public class ItemSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _keyAudioSource;
    [SerializeField] private AudioClip _onGrabClip;
    [SerializeField] private AudioClip _onCollisionClip;

    //public Methods
    public void PlayOnGrabSound()
    {
        _keyAudioSource.clip = _onGrabClip;
        _keyAudioSource.Play();
    }

    public void PlayOnCollisionClip()
    {
        _keyAudioSource.clip = _onCollisionClip;
        _keyAudioSource.Play();
    }
}
