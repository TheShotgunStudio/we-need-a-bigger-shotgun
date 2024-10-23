using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider EaseHealthSlider;
    public TextMeshProUGUI MaxHealthNumber;
    public TextMeshProUGUI RemainingHealthNumber;
    private float _lerpSpeed = 0.05f;

    public void Initialize(float maxHealth)
    {
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = maxHealth;
        EaseHealthSlider.maxValue = maxHealth;
        EaseHealthSlider.value = maxHealth;

        MaxHealthNumber.SetText("/" + HealthSlider.maxValue.ToString());
        RemainingHealthNumber.SetText(HealthSlider.value.ToString());
    }

    public void UpdateHealthBar(float currentHealth)
    {
        if (HealthSlider.value != currentHealth)
        {
            HealthSlider.value = currentHealth;
            RemainingHealthNumber.SetText(HealthSlider.value.ToString());
        }
    }

    private void Update()
    {
        if (HealthSlider.value != EaseHealthSlider.value)
        {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, HealthSlider.value, _lerpSpeed);
        }
    }
}
