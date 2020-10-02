using UnityEngine;

//this state must have nextState
public class ChaseTargetState : BasicState , ITargetExistState
{
    private Rigidbody rigidbody;
    private float speed = 5f; //temperal value

    public ChaseTargetState(Unit unit) : base(unit)
    {
        rigidbody = unit.GetComponent<Rigidbody>();
    }
    public override void Begin()
    {
        animator.Play("Run");
    }


    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime); 
        Vector3 direction = unit.DirectionToUnit(targetUnit);        
        direction.Normalize();
        rigidbody.velocity = direction * speed;       
        
    }
    protected override bool IsEnded()
    {
        if (unit.GetBaseAttackState(targetUnit).GetRange() >= unit.DistanceToUnit(targetUnit))
        {
            return true;
        }
        return false;
    }

    protected override void End() 
    {
        unit.SetState(unit.GetBaseAttackState(targetUnit));
        unit.GetState().Begin();
        SetNextState(null);
    }


    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public void OnTargetIsDead()
    {
        targetUnit = null;
    }
}
