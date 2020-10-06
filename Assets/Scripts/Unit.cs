using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	public event EventHandler OnTargeted;
	public event EventHandler OnDead;

	private Unit targetUnit;

	private Transform modelTransform;
	private BasicFXVixualizer BasicFXVisualizer;
	private List<Buff> buffs = new List<Buff>();

	private State state;
    #region State 
	private IdleState idleState;
	private MoveToGroundState moveToGroundState;
	private BaseAttackState baseAttackState;
	private ChaseTargetState chaseTargetState;

	public IdleState GetIdleState()
    {
		return idleState;
    }
	public MoveToGroundState GetMoveToGroundState(Vector3 destination)
    {
		moveToGroundState.SetDestination(destination);
		return moveToGroundState;
    }
	public BaseAttackState GetBaseAttackState()
    {
		return baseAttackState;			
    }
	public ChaseTargetState GetChaseTargetState()
    {
		return chaseTargetState;
    }

	public BasicState ProperBasicState()
    {
		if (TargetUnitExist())
		{
			if (baseAttackState.IsTargetUnitInRange())
			{
				return baseAttackState;
			}
			else
			{
				return chaseTargetState;
			}
		}
		else
		{
			return idleState;
		}
	}

	public void SetCurrentState(State state)
    {
		this.state = state;
    }
	public State GetCurrentState()
    {
		return state;
    }

	public void SetNextState(State nextState)
    {
		state.SetNextState(nextState);
    }

	#endregion

	public Action BaseAttackAction;

	protected virtual void Awake()
	{
		BasicFXVisualizer = GetComponent<BasicFXVixualizer>();
		#region State 
		state = idleState = new IdleState(this);
		moveToGroundState = new MoveToGroundState(this);
		baseAttackState = new BaseAttackState(this);
		chaseTargetState = new ChaseTargetState(this);
		#endregion
    }

    protected void Update()
	{
		foreach (Buff buff in buffs)
		{
			if (buff.IsEnded())
				buffs.Remove(buff);
			buff.Tick(Time.deltaTime);
		}
	}
	protected void FixedUpdate()
    {
		state.Tick(Time.fixedDeltaTime);
    }

	public Vector3 GetPosition()
    {
		return transform.position;
    }


	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	public abstract HealthPointsSystem GetHealthPointsSystem();

	public abstract void BeDamaged(int damage);



	public void Die()
	{				
		OnDead?.Invoke(this, EventArgs.Empty);
		BasicFXVisualizer.FlickFadeaway(1f, 10);
		Destroy(gameObject);
	}

	public BasicFXVixualizer GetBasicFXVisualizer()
    {
		return BasicFXVisualizer;
    }


	public void FlipLeft()
	{
		transform.localScale = Vector3.one;
	}

	public void FlipRight()
	{
		Vector3 scale = Vector3.one;
		scale.x = -1f;
		transform.localScale = scale;
	}

    public float ToTargetUnitDistance()
    {
        return Vector3.Distance(transform.position, targetUnit.GetPosition());
    }

    public Vector3 ToTargetUnitDirection()
    {
        Vector3 direction = targetUnit.GetPosition() - transform.position;
        direction.Normalize();
        return direction;
    }

    public bool TargetUnitExist()
    {
        if (null == targetUnit)
            return false;
        else
            return true;
    }

    public void SetTargetunit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public float DistanceToUnit(Unit unit)
    {
		return Vector3.Distance(transform.position, unit.GetPosition());
    }

	public Vector3 DirectionToUnit(Unit unit)
    {
		Vector3 direction = unit.GetPosition() - transform.position;
		direction.Normalize();
		return direction;
    }

	//public abstract float GetSpeed();

}