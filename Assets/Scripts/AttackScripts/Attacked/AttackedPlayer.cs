using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class AttackedPlayer : Attacked<Player>
{
    [SerializeField, Min(0)] private float _delayMovement = 0.5f;

    private WaitForSeconds _waitForSeconds;

    private bool _canMove = true;

    public bool CanMove => _canMove;

    protected override void Awake()
    {
        base.Awake();

        _waitForSeconds = new WaitForSeconds(_delayMovement);
    }

    public void BecomeImmobile()
    {
        _canMove = false;

        StartCoroutine(BecomeMobile());
    }

    private IEnumerator BecomeMobile()
    {
        yield return _waitForSeconds;

        _canMove = true;
    }
}