using UnityEngine;

public class Enemy : Unit
{
	[SerializeField]
	StatsDatum statsDatum;

	private Animator animator;
	private Unit targetUnit;



	private FSMState state = FSMState.Idle;


	#region FSM
	private enum FSMState
	{
		Idle,
		ChaseTarget,
		Attack
	}

	//음...프로토타입 패턴 써야하나..... 빌더 패턴 
	//프로토타임은 Clone()이라는 함수 만들어서  

    #endregion
    protected override void Awake()
	{
		base.Awake();
		statsSystem = new StatsSystem(statsDatum);
		animator = transform.Find("model").GetComponent<Animator>();
		healthPointsSystem = new HealthPointsSystem(statsSystem.GetTotalMaxHealthPoints());
	}

	private void FixedUpdate()
    {
        #region FSM
		switch(state)
        {
		case FSMState.Idle:
			animator.Play("Idle");
			break;
		case FSMState.ChaseTarget:
			animator.Play("Run");
			if(attackStrategy.GetRange() > DistanceToUnit(targetUnit))
            {
				state = FSMState.Attack;
            }
			break;
		case FSMState.Attack:
			if(attackStrategy.GetRange() <= DistanceToUnit(targetUnit))
            {
				state = FSMState.ChaseTarget;
				break;
            }
			attackStrategy.Attack(targetUnit);
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