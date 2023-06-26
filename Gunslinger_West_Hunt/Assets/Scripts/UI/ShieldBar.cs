using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetShield(int value)
    {
        slider.value = value;
        Debug.Log(slider.value);

    }

    public void ApplyMaxShield(int value)
    {
        slider.minValue = 0;
        slider.maxValue = value;
    }
}
