using GameUtility;
using UnityEngine;

public abstract class BasicState : State
{
    protected BasicState(Player player) : base(player) { }

    public override bool CanAccept(Command command)
    {
        return command.CanExecute();
    }


}

