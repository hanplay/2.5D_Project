using UnityEngine;

public class Enemy : Unit
{
	[SerializeField]
	StatsDatum statsDatum;

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
		statsSystem = new StatsSystem(statsDatum);
		healthPointsSystem = new HealthPointsSystem(statsSystem.GetTotalMaxHealthPoints());
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

    public override bool IsTargetable(Unit unit)
    {
		if (null == unit)
			return false;

		if (null != unit.GetComponent<Player>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
