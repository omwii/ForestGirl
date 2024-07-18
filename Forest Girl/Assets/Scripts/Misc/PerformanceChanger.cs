using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceChanger : MonoBehaviour
{
    [SerializeField] private int _targetFps = 60;
    [SerializeField] private int _vSyncCount = 0;
    [SerializeField] private AudioLowPassFilter _lowPass;

    private void Start()
    {
        Application.targetFrameRate = _targetFps;
        QualitySettings.vSyncCount = _vSyncCount;
        DOTween.To(() => _lowPass.cutoffFrequency, x => _lowPass.cutoffFrequency = x, 10000, 5);
    }
}
