using UnityEngine;

public class PhysicalCoin : PhysicsCustomizer<Coin>
{
	private const string CoinLayer = "Coin";
	
	private Rigidbody2D _rigidbody;

	protected override void GetComponents()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	protected override void ConfigureComponents()
	{
		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

		gameObject.layer = LayerMask.NameToLayer(CoinLayer);
	}
}