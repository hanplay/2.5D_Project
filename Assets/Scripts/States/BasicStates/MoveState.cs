using UnityEngine;
using GameUtility;



public class MoveState : BasicState
{
    private Rigidbody rigidbody;
    private float speed = 5f;

    public MoveState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
    }

    public override void Begin()
    {
        animator.Play("Run");
    }

    public override void Tick(float deltaTime)
    {
        rigidbody.velocity = inputBuffer.Direction() * speed;
        if (InputType.Attack == inputBuffer.GetInputType())
        {
            if (player.GetBaseAttackStrategy().GetRange() < player.DistanceToUnit(inputBuffer.GetTargetUnit()))
            {
                return;
            }
            else
            {
                player.SetState(player.GetAttackState());
                player.GetState().Begin();
            }
        }
 
        else if (InputType.Move == inputBuffer.GetInputType())
        {
            if(0.8f > Vector3.Distance(player.GetPosition(), inputBuffer.Destination()))
            {
                player.SetState(player.GetIdleState());
                player.GetState().Begin();
            }

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

  
}
