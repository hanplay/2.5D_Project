using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem 
{
    public delegate void MoveEventHandler(State senderState, Vector3 destination);
    public delegate void TargetUnitEventHandler(State senderState, Unit targetedUnit);
    public delegate void SkillEventHandler(State senderState, Skill skill);
    public delegate void TargetSkillEventHandler(State senderState, Skill skill, Unit targetedUnit);

    private Unit owner;



    private HealthPointsSystem healthPointsSystem;
    #region State
    private DieState dieState;
    private IdleState idleState;
    private MoveState moveState;
    private ChaseState chaseState;
    private AttackState attackState;

    private Stack<State> stateStack = new Stack<State>();
    public State GetCurrentState()
    {
        return stateStack.Peek();
    }

    public void PopState()
    {
        stateStack.Peek().End();
        stateStack.Pop();
    }
    public void PushState(State state)
    {
        stateStack.Peek().End();
        stateStack.Push(state);
    }

    public DieState GetDieState()
    {
        return dieState;
    }
    public IdleState GetIdleState()
    {
        return idleState;
    }
    public MoveState GetMoveState()
    {
        return moveState;
    }
    public ChaseState GetChaseState()
    {
        return chaseState;
    }
    public AttackState GetAttackState()
    {
        return attackState;
    }
    #endregion

    public StateSystem(Unit owner)
    {
        this.owner = owner;
        dieState =      new DieState    (owner, this, 5f);
        idleState =     new IdleState   (owner, this);
        moveState =     new MoveState   (owner, this);
        chaseState =    new ChaseState  (owner, this);
        attackState =   new AttackState (owner, this);
        healthPointsSystem = owner.GetHealthPointsSystem();


        idleState.OnMove += IdleState_OnMove;
        idleState.OnChase += IdleState_OnChase;
        idleState.OnSkill += BasicState_OnSkill;

        moveState.OnChase += MoveState_OnChase;
        moveState.OnSkill += BasicState_OnSkill;

        chaseState.OnMove += ChaseState_OnMove;
        chaseState.OnAttack += ChaseState_OnAttack;
        chaseState.OnSkill += BasicState_OnSkill;
        chaseState.OnTargetingSkill += BasicState_OnTargetingSkill;

        attackState.OnMove += AttackState_OnMove;
        attackState.OnChase += AttackState_OnChase;
        attackState.OnSkill += BasicState_OnSkill;
        attackState.OnTargetingSkill += BasicState_OnTargetingSkill;
        attackState.OnTargetingSkillReserve += AttackState_OnTargetingSkillReserve;

        healthPointsSystem.OnDead += HealthPointsSystem_OnDead;

        stateStack.Push(dieState);
        stateStack.Push(idleState);


    }




    #region State Change
    private void BasicState_OnSkill(State senderState, Skill skill)
    {
        PushState(skill.GetSkillState());
    }
    private void BasicState_OnTargetingSkill(State senderState, Skill skill, Unit targetedUnit)
    {
        ISkillTargetingState skillTargetingState = skill.GetSkillState() as ISkillTargetingState;
        skillTargetingState.SetTarget(targetedUnit);
        PushState(skill.GetSkillState());
    }

    private void IdleState_OnMove(State senderState, Vector3 destination)
    {
        PushState(moveState);
        moveState.MoveTo(destination);
    }

    private void IdleState_OnChase(State senderState, Unit targetedUnit)
    {
        if (owner.GetAttackSystem().GetRange() < owner.DistanceToUnit(targetedUnit))
        {            
            PushState(chaseState);
            chaseState.ChaseTarget(targetedUnit);
        }
        else
        {           
            PushState(attackState);
            attackState.Attack(targetedUnit);
        }
    }
    private void MoveState_OnChase(State senderState, Unit targetedUnit)
    {
        if(owner.GetAttackSystem().GetRange() < owner.DistanceToUnit(targetedUnit))
        {
            PopState();
            PushState(chaseState);
            chaseState.ChaseTarget(targetedUnit);
        }
        else
        {
            PopState();
            PushState(attackState);
            attackState.Attack(targetedUnit);
        }
    }

    private void ChaseState_OnMove(State senderState, Vector3 destination)
    {
        chaseState.ReleaseTarget();
        PopState();
        PushState(moveState);
        moveState.MoveTo(destination);
    }
    
    private void ChaseState_OnAttack(State senderState, Unit targetedUnit)
    {
        chaseState.ReleaseTarget();
        PopState();
        PushState(attackState);
        attackState.Attack(targetedUnit);
    }

    private void AttackState_OnMove(State senderState, Vector3 destination)
    {
        attackState.ReleaseTarget();
        PopState();
        PushState(moveState);
        moveState.MoveTo(destination);
    }
    private void AttackState_OnChase(State senderState, Unit targetedUnit)
    {        
        attackState.ReleaseTarget();
        PopState();
        PushState(chaseState);
        chaseState.ChaseTarget(targetedUnit);
        
    }
    private void AttackState_OnTargetingSkillReserve(State senderState, Skill skill, Unit targetedUnit)
    {
        ISkillTargetingState skillTargetingState = skill.GetSkillState() as ISkillTargetingState;
        skillTargetingState.SetTarget(targetedUnit);
        PopState();
        chaseState.ReserveTargetingSkill(skill);
        PushState(chaseState);
    }
    private void HealthPointsSystem_OnDead(object sender, System.EventArgs e)
    {
        while (1 != stateStack.Count)
            stateStack.Pop();               
    }

    #endregion

    public void Tick(float deltaTime)
    {
        if(false == stateStack.Peek().IsBegun())
        {
            stateStack.Peek().Begin();
            stateStack.Peek().Tick(deltaTime);
        }
        else
        {
            stateStack.Peek().Tick(deltaTime);
        }        
    }
}
