using UnityEngine;

public abstract class State 
{
    protected Player player;
    protected Unit targetUnit;
    protected Animator animator;

    public State(Player player)
    {
        this.player = player;
        animator = player.transform.Find("model").GetComponent<Animator>();
    }
    public abstract void Begin();

    /* TickAccept함수는 deltaTime마다 command(visitor)를 받고 실행됨*/
    public abstract void TickAccept(float deltaTime, Command command);
 

    public void ChangeToIdleState()
    {
        player.SetState(player.GetIdleState());
        player.GetState().Begin();
    }
    public void ChangeToMoveState()
    {
        player.SetState(player.GetMoveState());
        player.GetState().Begin();
    }

    public void ChangeToAttackState(Unit targetUnit)
    {
        player.SetState(player.GetAttackState(targetUnit));
        player.GetState().Begin();
    }

    public void ChageToSkillState(SkillState skillState)
    {
        player.SetState(skillState);
        player.GetState().Begin();
    }

    public bool IsTargetUnit()
    {
        if (null == targetUnit)
            return false;
        else
            return true;
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

}
