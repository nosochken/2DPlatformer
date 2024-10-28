using UnityEngine;

public class VampirismDisplay : MonoBehaviour
{
    private Vampirism _vampirism;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _vampirism = GetComponent<Vampirism>();
        _sprite = GetComponent<SpriteRenderer>();

        _sprite.enabled = false;
    }

    private void OnEnable()
    {
        _vampirism.AbilityUsing += (value, maxValue) => _sprite.enabled = true;
        _vampirism.AbilityRecharging += (value, maxValue) => _sprite.enabled = false;
    }

    private void OnDisable()
    {
        _vampirism.AbilityUsing -= (value, maxValue) => _sprite.enabled = true;
        _vampirism.AbilityRecharging -= (value, maxValue) => _sprite.enabled = false;
    }
}