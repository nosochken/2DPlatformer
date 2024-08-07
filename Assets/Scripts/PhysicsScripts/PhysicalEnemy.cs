using UnityEngine;

public class PhysicalEnemy : PhysicsCustomizer<Enemy>
{
	private const string EnemyLayer = "Enemy";
	
	private Rigidbody2D _rigidbody;
	
	protected override void GetComponents()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	protected override void ConfigureComponents()
	{
		_rigidbody.bodyType = RigidbodyType2D.Dynamic;
		_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

		gameObject.layer = LayerMask.NameToLayer(EnemyLayer);
	}
}