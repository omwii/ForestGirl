using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class BasementLights : MonoBehaviour
{
    [SerializeField] private Volume _basementVolume;
    [SerializeField] private Light[] _basementLights;

    private float _basementIntensity;

    private void Start()
    {
        _basementIntensity = _basementLights[0].intensity;
        foreach (Light light in _basementLights)
        {
            light.DOIntensity(0, 0.1f);
        }
    }

    public void LightBasement()
    {
        foreach (var light in _basementLights) 
        {
            light.DOIntensity(_basementIntensity, 2f);
        }
        ﻿﻿﻿﻿﻿﻿﻿DOTween.To(()=> _basementVolume.weight, x=> _basementVolume.weight = x, 0, 2);
    }
}
