public class PlayerHealth : Health<Player>
{
	public void Recover(float value)
	{
		if (value <= 0)
			return;
		
		CurrentValue += value;
		
		TryReduceToMaxValue();
	}
	
	private void TryReduceToMaxValue()
	{
		if (CurrentValue > MaxValue)
			CurrentValue = MaxValue;
	}
}