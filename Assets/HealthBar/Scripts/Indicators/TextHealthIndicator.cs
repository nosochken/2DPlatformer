using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthIndicator : HealthIndicator
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void Display(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth} / {maxHealth}";
    }
}