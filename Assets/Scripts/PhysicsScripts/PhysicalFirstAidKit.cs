using UnityEngine;

public class PhysicalFirstAidKit : PhysicsCustomizer<FirstAidKit>
{
	private const string FirstAidKitLayer = "FirstAidKit";
	
	private Rigidbody2D _rigidbody;
	
	protected override void GetComponents()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	protected override void ConfigureComponents()
	{
		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		_rigidbody.gravityScale = 0;
		_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

		gameObject.layer = LayerMask.NameToLayer(FirstAidKitLayer);
	}
}