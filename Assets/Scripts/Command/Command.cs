using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command 
{
    protected Player player;
    protected int commandMode;

    public bool HasProperty(int commandMode)
    {
        /*
            0 oper 0 -> 1
            0 oper 1 -> 0
            1 oper 0 -> 1
            1 oper 1 -> 1                            
        */
        int bitMask = (this.commandMode & (~commandMode)) | (this.commandMode | (~commandMode));
        if (int.MaxValue == bitMask)
        {
            return true;
        }
        else
            return false;
    }
    public abstract bool CanExecute();
    public abstract void Execute();

    private Command nextCommand;

    public Command(Player player)
    {
        this.player = player;
    }

    public int GetCommandMode()
    {
        return commandMode;
    }

}
