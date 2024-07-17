using UnityEngine;

public class ItemSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _keyAudioSource;
    [SerializeField] private AudioClip _onCollisionClip;

    public void PlayOnCollisionClip()
    {
        _keyAudioSource.clip = _onCollisionClip;
        _keyAudioSource.Play();
    }
}
