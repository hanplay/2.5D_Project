using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BasicState
{
    private Rigidbody rigidbody;
    private float speed = 5f;

    public ChaseState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
    }

    public override void Begin()
    {
    }

    public override void TickAccept(float deltaTime, Command command)
    {
        animator.Play("Run");
        command.Visit(this);
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    //public void RefreshUnitBuffer()
    //{
    //    targetUnit = null;
    //}


    public void MoveToTargetUnit()
    {
        Vector3 direction = targetUnit.GetPosition() - player.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * speed;
    }

}
