using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStander))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField, Min(1)] private float _jumpForce = 11f;

    private Rigidbody2D _rigidbody;

    private WaitForFixedUpdate _wait;

    public event Action<bool> IsJumping;

    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();

        _wait = new WaitForFixedUpdate();
    }

    public IEnumerator Jump()
    {
        IsJumping?.Invoke(true);

        float maxHeight = transform.position.y;

        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        while (transform.position.y >= maxHeight)
        {
            maxHeight = transform.position.y;
            yield return _wait;
        }

        IsJumping?.Invoke(false);
    }
}