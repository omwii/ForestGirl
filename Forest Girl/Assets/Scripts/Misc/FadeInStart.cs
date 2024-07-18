using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStart : MonoBehaviour
{
    [SerializeField] private Image _fadeInImage;

    private void Start()
    {
        _fadeInImage.color = Color.black;
        _fadeInImage.DOFade(0, 2);
    }
}
