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
 

    public void ChangeToDieState()
    {
        player.SetState(player.GetDieState());
        player.GetState().Begin();
    }

    public void ChageToSkillState(SkillState skillState)
    {
        player.SetState(skillState);
        player.GetState().Begin();
    }


    public abstract bool IsTargetingState();


    public Unit GetTargetUnit()
    {
        return targetUnit;
    }


}
