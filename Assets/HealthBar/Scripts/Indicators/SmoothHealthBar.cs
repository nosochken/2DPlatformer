using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _duration = 1f;

    private Coroutine _currentCoroutine;

    protected override void DisplaySliderOf(float currentHealth, Slider slider)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(ChangeSmoothly(currentHealth, slider));
    }

    private IEnumerator ChangeSmoothly(float currentHealth, Slider slider)
    {
        float elapsedTime = 0f;
        float startValue = slider.value;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startValue, currentHealth, elapsedTime / _duration);
            yield return null;
        }
    }
}