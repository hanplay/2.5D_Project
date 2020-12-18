using UnityEngine;

public class Enemy : Unit
{
	[SerializeField]
	StatsDatum statsDatum;


	//음...프로토타입 패턴 써야하나..... 빌더 패턴 
	//프로토타임은 Clone()이라는 함수 만들어서  

    protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(4, 0);
		healthPointsSystem.Init(200);
		moveSystem.Init(new StraightMoveStrategy(this), 3f);
		attackSystem.Init(new InstantAttackStrategy(this, new CommonDamageStrategy()), 1.5f);
	}

    private void FixedUpdate()
    {
	
    }

    public override bool IsTargetable(Unit unit)
    {
		if (null == unit)
			return false;

		if (null != unit.GetComponent<Unit>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}