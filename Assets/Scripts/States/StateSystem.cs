using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem 
{
    public delegate void MoveEventHandler(State senderState, Vector3 destination);
    public delegate void TargetUnitEventHandler(State senderState);
    public delegate void SkillEventHandler(State senderState, Skill skill);
    public delegate void TargetSkillEventHandler(State senderState, Skill skill);

    private Unit owner;

    private HealthPointsSystem healthPointsSystem;
    private TargetedUnitHandler targetedUnitHandler;
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
        if(false == stateStack.Peek().IsEnded())
            stateStack.Peek().End();
        stateStack.Pop();
    }
    public void PushState(State state)
    {
        if (false == stateStack.Peek().IsEnded())
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
        dieState =      new DieState    (owner, this);
        idleState =     new IdleState   (owner, this);
        moveState =     new MoveState   (owner, this);
        chaseState =    new ChaseState  (owner, this);
        attackState =   new AttackState (owner, this);
        healthPointsSystem = owner.GetHealthPointsSystem();
        targetedUnitHandler = owner.GetTargetedUnitHandler();


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
    private void BasicState_OnTargetingSkill(State senderState, Skill skill)
    {
        PushState(skill.GetSkillState());
    }

    private void IdleState_OnMove(State senderState, Vector3 destination)
    {
        PushState(moveState);
        moveState.MoveTo(destination);
    }

    private void IdleState_OnChase(State senderState)
    {
        if (true == targetedUnitHandler.TargetInProperRange(out bool isTooClose))
        {            
            PushState(attackState);
        }
        else
        {           
            PushState(chaseState);
        }
    }
    private void MoveState_OnChase(State senderState)
    {
        if(true == targetedUnitHandler.TargetInProperRange(out bool isTooClose))
        {
            PopState();
            PushState(chaseState);
        }
        else
        {
            PopState();
            PushState(attackState);
        }
    }

    private void ChaseState_OnMove(State senderState, Vector3 destination)
    {     
        PopState();
        PushState(moveState);
        moveState.MoveTo(destination);
    }
    
    private void ChaseState_OnAttack(State senderState)
    {        
        PopState();
        PushState(attackState);
    }

    private void AttackState_OnMove(State senderState, Vector3 destination)
    {
        PopState();
        PushState(moveState);
        moveState.MoveTo(destination);
    }
    private void AttackState_OnChase(State senderState)
    {        
        PopState();
        PushState(chaseState);        
    }
    private void AttackState_OnTargetingSkillReserve(State senderState, Skill skill)
    {
        PopState();
        chaseState.ReserveTargetingSkill(skill);
        PushState(chaseState);
    }
    private void HealthPointsSystem_OnDead(Unit unit)
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
