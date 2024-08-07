using UnityEngine;

[RequireComponent(typeof(FirstAidKit))]
public class DetectFirstAidKit : Detection<FirstAidKit> 
{
	private FirstAidKit _firstAidKit;
	
	public float HealingValue => _firstAidKit.HealingValue;
	
	private void Awake()
	{
		_firstAidKit = GetComponent<FirstAidKit>();
	}
}