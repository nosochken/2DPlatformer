using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private float _direction;

    public event Action<float> DirectionChanged;
    public event Action JumpKeyPressed;

    private void Update()
    {
        ReadDirectionChange();
        ReadJumpKey();
    }

    private void ReadDirectionChange()
    {
        float previousDirection = _direction;
        _direction = Input.GetAxis(HorizontalAxis);

        if (_direction != previousDirection)
            DirectionChanged?.Invoke(_direction);
    }

    private void ReadJumpKey()
    {
        if (Input.GetKeyDown(_jumpButton))
            JumpKeyPressed?.Invoke();
    }
}