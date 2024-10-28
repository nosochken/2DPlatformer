using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VampirismBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _vampirism.AbilityUsing += Display;
        _vampirism.AbilityRecharging += Display;
    }

    private void OnDisable()
    {
        _vampirism.AbilityUsing -= Display;
        _vampirism.AbilityRecharging -= Display;
    }

    private void Display(float value, float maxValue)
    {
        _slider.value = value / maxValue;
    }
}