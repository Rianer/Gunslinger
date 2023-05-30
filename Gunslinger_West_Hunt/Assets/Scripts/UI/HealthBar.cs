using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log(slider.value);

    }

    public void ApplyMaxHealth(int value)
    {
        slider.minValue = 0;
        slider.maxValue = value;
    }
}
