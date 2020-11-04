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

    public override bool IsTargetingState()
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
        MoveStraightLine(destination);
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
        MoveStraightLine(targetUnit.GetPosition());
    }

    public void AttackTarget(Unit targetUnit)
    {
        fsmState = FSMState.Attack;
        this.targetUnit = targetUnit;
        if (prevFsmState != fsmState)
        {
            prevFsmState = fsmState;
            player.GetAttackStrategy().Attack(targetUnit);
        }
    }

    public void Stop()
    {
        targetUnit = null;
        fsmState = FSMState.Idle;
        if (prevFsmState != fsmState)
        {
            prevFsmState = fsmState;
        }
        animator.Play("Idle");
    }

    private void MoveStraightLine(Vector3 destination)
    {
        Vector3 direction = destination - player.GetPosition();
        if (direction.x < 0f)
            player.FlipLeft();
        else
            player.FlipRight();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = statsSystem.GetTotalMoveSpeed() * direction;
    }
}