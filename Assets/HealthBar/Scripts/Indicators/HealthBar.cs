using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthIndicator
{
    private Slider _slider;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void Display(float currentHealth, float maxHealth)
    {
        float currentHealthForSlider = currentHealth / maxHealth;
        DisplaySliderOf(currentHealthForSlider, _slider);
    }

    protected virtual void DisplaySliderOf(float currentHealth, Slider slider)
    {
        _slider.value = currentHealth;
    }
}