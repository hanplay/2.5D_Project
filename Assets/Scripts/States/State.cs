using UnityEngine;

public abstract class State 
{
    protected Player player;
    protected Animator animator;
    protected Unit targetUnit;
    protected Command command;

    public State(Player player)
    {
        this.player = player;
        animator = player.transform.Find("model").GetComponent<Animator>();
    }

    public virtual void Tick(float deltaTime)
    {
        if (null != command)
        {
            if(CanAccept(command))
            {
                command.Execute();
                command = null;
            }
            else
            {

            }
        }

        if(IsEnded())
        {
            End();
            command = null;
        }
    }

    public abstract bool CanAccept(Command command);


    public abstract void Begin();

    protected abstract void End();
    protected abstract bool IsEnded();
    public bool IsTargetUnitExist()
    {
        if (null == targetUnit)
            return false;
        else
            return true;
    }

    public void SetCommand(Command command)
    {
        this.command = command;
    }

}
