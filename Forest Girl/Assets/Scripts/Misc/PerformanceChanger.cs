using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceChanger : MonoBehaviour
{
    [SerializeField] private int _targetFps = 60;
    [SerializeField] private int _vSyncCount = 0;

    private void Awake()
    {
        Application.targetFrameRate = _targetFps;
        QualitySettings.vSyncCount = _vSyncCount;
    }
}
