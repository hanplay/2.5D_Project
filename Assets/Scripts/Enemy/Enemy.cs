using UnityEngine;

public class Enemy : Unit
{
	private HealthPointsSystem healthPointsSystem;

	


	protected void Awake()
	{
		base.Awake();
		healthPointsSystem = new HealthPointsSystem(100);
	}

	public override void BeDamaged(int damage)
	{
		
	}



	public override HealthPointsSystem GetHealthPointsSystem()
	{
		return healthPointsSystem;
	}
}
