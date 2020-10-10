using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected Player player;
    protected Animator animator;
    protected InputBuffer inputBuffer;
    protected int stateMode;


    public State(Player player)
    {
        this.player = player;
        inputBuffer = player.GetInputHandler();
        animator = player.transform.Find("model").GetComponent<Animator>();

    }

    public abstract void Begin();
    public abstract void Tick(float deltaTime);
    public int GetStateMode()
    {
        return stateMode;
    }

    public bool HasProperty(int stateMode)
    {
        /*
            0 oper 0 -> 1
            0 oper 1 -> 0
            1 oper 0 -> 1
            1 oper 1 -> 1                            
        */
        int bitMask = (this.stateMode & (~stateMode)) | (this.stateMode | (~stateMode));
        if (-1 == bitMask) // -1 -> 1111 1111 1111 1111 1111 1111 1111 1111
        {
            return true;
        }
        else
            return false;
    }
}
