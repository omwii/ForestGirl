using UnityEngine;

public class InteractAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayInteractAudio()
    {
        _audioSource.Play();
    }
}
