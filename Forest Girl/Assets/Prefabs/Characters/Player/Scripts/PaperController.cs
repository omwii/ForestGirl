using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaperController : MonoBehaviour
{
    [SerializeField] private GameObject[] _paperImages;
    [SerializeField] private GameObject[] _paperStages;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") != 0)
        {
            for (int i = 0; i < _paperImages.Length; i++)
            {
                _paperImages[i].SetActive(false);
            }
            for (int i = 0; i < _paperStages.Length; i++)
            {
                _paperStages[i].SetActive(false);
            }
        }
    }

    public void OpenPaper(int paperStage)
    {
        for (int i = 0; i < _paperImages.Length; i++)
        {
            _paperImages[i].SetActive(true);
        }
        for (int i = 0; i < _paperStages.Length; i++)
        {
            _paperStages[i].SetActive(false);
            if (i == paperStage)
                _paperStages[i].SetActive(true);
        }
    }
}
