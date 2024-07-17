using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPerformer
{
    public bool IsRunning = false;
    public float StaminaValue { get; private set; }
    public float _maxStaminaValue { get; private set; }
    
    private float _staminaRegenSpeed;
    private float _staminaWasteSpeed;

    public void StaminaLogic()
    {
        if (IsRunning && StaminaValue - Time.deltaTime >= 0)
        {
            StaminaValue -= Time.deltaTime * _staminaWasteSpeed;
        }
        else if (StaminaValue + Time.deltaTime <= _maxStaminaValue)
        {
            StaminaValue += Time.deltaTime * _staminaRegenSpeed;
        }
    }

    //Constructor
    public StaminaPerformer(float maxStaminaValue,
        float staminaRegenSpeed,
        float staminaWasteSpeed) 
    {
        _maxStaminaValue = maxStaminaValue;
        _staminaRegenSpeed = staminaRegenSpeed;
        _staminaWasteSpeed = staminaWasteSpeed;

        StaminaValue = _maxStaminaValue;
    }
}
