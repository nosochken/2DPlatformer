using UnityEngine;

public class DetectPlayer : Detect<Player>
{
	private Vector2 _startPosition;

	private void Start()
	{
		_startPosition = transform.position;
	}

	public override void WasDetectedBy<T>(Detector<T> detector)
	{
		if (detector.gameObject.TryGetComponent<BottomBorder>(out _))
			transform.position = _startPosition;
	}
}