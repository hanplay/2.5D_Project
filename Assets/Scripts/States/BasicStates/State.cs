using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected Player player;
    protected Animator animator;


    public State(Player player)
    {
        this.player = player;
        animator = player.transform.Find("model").GetComponent<Animator>();

    }

    public abstract void Begin();
    public abstract void TickAccept(float deltaTime, Command command);
 

    public void ChangeToMoveState()
    {
        player.SetState(player.GetMoveState());
        player.GetState().Begin();
    }

    public void ChangeToAttackState()
    {
        player.SetState(player.GetAttackState());
        player.GetState().Begin();
    }

    public void ChangeToIdleState()
    {
        player.SetState(player.GetIdleState());
        player.GetState().Begin();
    }
}
