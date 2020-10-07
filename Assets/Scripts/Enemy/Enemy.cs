using UnityEngine;

public class Enemy : Unit
{
	private HealthPointsSystem healthPointsSystem;
	private float range;
	private FSMState state = FSMState.Idle;
	#region FSM
	private enum FSMState
	{
		Idle,
		ChaseTarget,
		Attack
	}

    #endregion

    protected void Awake()
	{
		base.Awake();
		healthPointsSystem = new HealthPointsSystem(100);
	}

	private void FixedUpdate()
    {
        #region FSM
		switch(state)
        {
		case FSMState.Idle:
			break;
		case FSMState.ChaseTarget:
			break;
		case FSMState.Attack:
			break;
        }
        #endregion
    }

    public override void BeDamaged(int damage)
	{
		
	}

	public override HealthPointsSystem GetHealthPointsSystem()
	{
		return healthPointsSystem;
	}
}
