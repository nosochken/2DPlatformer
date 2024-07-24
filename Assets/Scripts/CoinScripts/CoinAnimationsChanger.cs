using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Coin))]
public class CoinAnimationsChanger : MonoBehaviour
{
    private Animator _animator;
    private Coin _coin;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _coin = GetComponent<Coin>();
    }

    private void OnEnable()
    {
        _coin.IsShining += ChangeShineState;
    }

    private void OnDisable()
    {
        _coin.IsShining -= ChangeShineState;
    }

    private void ChangeShineState(bool isShining)
    {
        _animator.SetBool(CoinAnimatorData.Params.IsShining, isShining);
    }
}