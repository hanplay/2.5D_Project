using UnityEngine;

public class BasicState : State
{
    
    private Rigidbody rigidbody;
    private Vector3 destination;
    private StatsSystem statsSystem;
    private enum FSMState
    {
        Idle,
        Chase,
        Move,
        Attack
    }
    private FSMState fsmState;
    private FSMState prevFsmState;
    

    public BasicState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
        statsSystem = player.GetStatsSystem();
    }

    public override void Begin() { }

    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);

    }

    public override bool IsTargetIngState()
    {
        if (null == targetUnit)
            return false;
        else
            return true;
    }

    public void MoveTo(Vector3 destination)
    {
        targetUnit = null;
        fsmState = FSMState.Move;
        this.destination = destination;
        if(prevFsmState != fsmState)
        {
            prevFsmState = fsmState;
        }
        animator.Play("Run");
        MoveStarightLine(destination);
    }

    public void ChaseTarget(Unit targetUnit)
    {
        fsmState = FSMState.Chase;
        this.targetUnit = targetUnit;
        if (prevFsmState != fsmState)
        {
            prevFsmState = fsmState;
        }   
        animator.Play("Run");
        MoveStarightLine(targetUnit.GetPosition());
    }

    public void AttackTarget(Unit targetUnit)
    {
        fsmState = FSMState.Attack;
        this.targetUnit = targetUnit;
        if (prevFsmState != fsmState)
        {
            player.BaseAttackAction = BaseAttackDamage;
            prevFsmState = fsmState;
            animator.Play("Attack");
        }
    }

    private void BaseAttackDamage()
    {
        Debug.Log("Damage");
        //argetUnit.BeDamaged(statsSystem.GetTotalAttackPower());
    }

    public void Stop()
    {
        targetUnit = null;
        fsmState = FSMState.Idle;
        if (prevFsmState != fsmState)
        {
            prevFsmState = fsmState;
            animator.Play("Idle");
        }
    }

    private void MoveStarightLine(Vector3 destination)
    {
        Vector3 direction = destination - player.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = statsSystem.GetTotalMoveSpeed() * direction;
    }


}
