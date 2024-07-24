public class SpawnZone
{
    private float _leftmostX;
    private float _rightmostX;
    private float _y;

    public float LeftmostX => _leftmostX;
    public float RightmostX => _rightmostX;
    public float Y => _y;

    public void Initialize(float leftmostX, float rightmostX, float y)
    {
        _leftmostX = leftmostX;
        _rightmostX = rightmostX;
        _y = y;
    }
}