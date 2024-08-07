using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStander))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField, Min(1)] private float _jumpForce = 11f;

    private PlayerStander _stander;

    private Rigidbody2D _rigidbody;

    private bool _shouldJump;

    private WaitForFixedUpdate _wait;

    public event Action<bool> IsJumping;

    private void Awake()
    {
        _stander = GetComponent<PlayerStander>();

        _rigidbody = GetComponent<Rigidbody2D>();

        _wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        _inputReader.JumpKeyPressed += TryChangeJumpingState;
    }

    private void OnDisable()
    {
        _inputReader.JumpKeyPressed -= TryChangeJumpingState;
    }

    public void Jump()
    {
        if (_shouldJump)
            StartCoroutine(PerformJump());
    }

    private void TryChangeJumpingState()
    {
        if (_stander.IsOnPlatform)
            _shouldJump = true;
    }

    private IEnumerator PerformJump()
    {
        IsJumping?.Invoke(_shouldJump);

        float maxHeight = transform.position.y;

        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        _shouldJump = false;

        while (transform.position.y >= maxHeight)
        {
            maxHeight = transform.position.y;
            yield return _wait;
        }

        IsJumping?.Invoke(_shouldJump);
    }
}