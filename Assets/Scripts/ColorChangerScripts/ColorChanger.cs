using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _spriteRenderer.color = _defaultColor;
    }

    public IEnumerator ChangeColorForWhile(WaitForSeconds waitForSeconds, Color color)
    {
        _spriteRenderer.color = color;
        yield return waitForSeconds;

        _spriteRenderer.color = _defaultColor;
    }
}