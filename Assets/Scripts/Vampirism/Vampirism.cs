using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private const string EnemyLayer = "Enemy";
    private const float RadiusToDiameterRatio = 2f;

    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _rechargeTime = 4f;
    [SerializeField] private float _abilityRadius = 4f;
    [SerializeField] private float _damagePerSecond = 4f;

    private Health _health;
    private LayerMask _enemyLayerMask;
    private Coroutine _coroutineOfAbility;

    private float _startTimeValue = 0f;
    private float _abilityTimeLeft;
    private float _cooldownTimeLeft;

    public event Action<float, float> AbilityUsing;
    public event Action<float, float> AbilityRecharging;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();

        _enemyLayerMask = LayerMask.GetMask(EnemyLayer);
        transform.localScale = Vector2.one * _abilityRadius * RadiusToDiameterRatio;

        _abilityTimeLeft = _abilityDuration;
        _cooldownTimeLeft = _rechargeTime;
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

            AttackedEnemy nearestEnemy = FindNearestEnemy();

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

    private AttackedEnemy FindNearestEnemy()
    {
        float shortestDistanceSqr = Mathf.Infinity;
        Collider2D nearestEnemyCollider = null;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemyLayerMask);

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                float distanceToEnemySqr = (transform.position - enemy.transform.position).sqrMagnitude;

                if (distanceToEnemySqr < shortestDistanceSqr)
                {
                    shortestDistanceSqr = distanceToEnemySqr;
                    nearestEnemyCollider = enemy;
                }
            }
        }

        return nearestEnemyCollider ? nearestEnemyCollider.GetComponent<AttackedEnemy>() : null;
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