using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BasicState
{
    private float duration;
    private float lagTime;
    public AttackState(Player player) : base(player) { }
 

    public override void Begin()
    {
        player.BaseAttackAction = Work;
        animator.Play("Attack");
        if(0 == duration)
            duration = player.GetClipLength("Attack");
    }

    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;

        if (InputType.Attack == inputBuffer.GetInputType())
        {
            if (player.GetBaseAttackStrategy().GetRange() > player.DistanceToUnit(inputBuffer.GetTargetUnit()))
            {
                if(lagTime >= duration)
                {
                    lagTime = 0f;
                    Begin();
                }
  
            }
            else
            {
                player.SetState(player.GetMoveState());
                player.GetState().Begin();
            }
        }

        else if (InputType.Move == inputBuffer.GetInputType())
        {
            player.SetState(player.GetMoveState());
            player.GetState().Begin();
        }
        else if (InputType.Skill == inputBuffer.GetInputType())
        {

        }
        else //(InputType.None == inputHandler.GetInputType())
        {
            player.SetState(player.GetIdleState());
            player.GetState().Begin();
        }
    }

    private void Work()
    {
        Debug.Log("Damage!");
    }
}
