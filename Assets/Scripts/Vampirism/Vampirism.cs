using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AttackedEnemyDetector))]
public class Vampirism : MonoBehaviour
{
    private const float RadiusToDiameterRatio = 2f;

    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _rechargeTime = 4f;
    [SerializeField] private float _abilityRadius = 4f;
    [SerializeField] private float _damagePerSecond = 4f;

    private AttackedEnemyDetector _attackedEnemyDetector;
    private Health _health;
    private Coroutine _coroutineOfAbility;

    private float _startTimeValue = 0f;
    private float _abilityTimeLeft;
    private float _cooldownTimeLeft;

    public event Action<float, float> AbilityUsing;
    public event Action<float, float> AbilityRecharging;

    private void Awake()
    {
        _attackedEnemyDetector = GetComponent<AttackedEnemyDetector>();
        _health = GetComponentInParent<Health>();

        transform.localScale = Vector2.one * _abilityRadius * RadiusToDiameterRatio;

        _abilityTimeLeft = _abilityDuration;
        _cooldownTimeLeft = _rechargeTime;
    }

    private void Start()
    {
        AbilityRecharging?.Invoke(_cooldownTimeLeft, _rechargeTime);
    }

    public void Activate()
    {
        if (_cooldownTimeLeft >= _rechargeTime)
        {
            _cooldownTimeLeft = _startTimeValue;

            if (_coroutineOfAbility != null)
                StopCoroutine(_coroutineOfAbility);

            _coroutineOfAbility = StartCoroutine(UseAbility());
        }
    }

    private IEnumerator UseAbility()
    {
        float damageAccumulator = 0f;
        float fullAccumulator = 1f;

        while (_abilityTimeLeft > _startTimeValue)
        {
            _abilityTimeLeft -= Time.deltaTime;
            AbilityUsing?.Invoke(_abilityTimeLeft, _abilityDuration);

            AttackedEnemy nearestEnemy = _attackedEnemyDetector.FindNearestDetectable(_enemyLayerMask, _abilityRadius);

            if (nearestEnemy != null)
            {
                float damageDealt = _damagePerSecond * Time.deltaTime;
                damageAccumulator += damageDealt;

                if (damageAccumulator >= fullAccumulator)
                {
                    int wholeDamageToApply = Mathf.FloorToInt(damageAccumulator);

                    nearestEnemy.TakeDamage(wholeDamageToApply);
                    _health.Increase(wholeDamageToApply);

                    damageAccumulator -= wholeDamageToApply;
                }
            }

            yield return null;
        }

        StartCoroutine(ChargeAbility());
    }

    private IEnumerator ChargeAbility()
    {
        while (_cooldownTimeLeft < _rechargeTime)
        {
            _cooldownTimeLeft += Time.deltaTime;
            AbilityRecharging?.Invoke(_cooldownTimeLeft, _rechargeTime);
            yield return null;
        }

        _abilityTimeLeft = _abilityDuration;
    }
}