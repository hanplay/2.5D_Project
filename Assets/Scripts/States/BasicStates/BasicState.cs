using GameUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicState : State
{
    public BasicState(Player player) : base(player) 
    {
        stateMode = StateMode.Basic;
    }


}
