using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _vampirismKey = KeyCode.Q;

    private float _direction;

    public event Action<float> DirectionChanged;
    public event Action JumpKeyPressed;
    public event Action VampirismKeyPressed;

    private void Update()
    {
        ReadDirectionChange();
        ReadJumpKey();
        ReadVampirismKey();
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
        if (Input.GetKeyDown(_jumpKey))
            JumpKeyPressed?.Invoke();
    }

    private void ReadVampirismKey()
    {
        if (Input.GetKeyDown(_vampirismKey))
            VampirismKeyPressed?.Invoke();
    }
}