using UnityEngine;

[RequireComponent(typeof(PhysicalFirstAidKit), typeof(DetectFirstAidKit), typeof(SpawnFirstAidKit))]
public class FirstAidKit : MonoBehaviour
{
	[SerializeField, Min(1) ]private float _healingValue = 5f;
	
	public float HealingValue => _healingValue;
}
