using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCommand : Command
{
    public NullCommand(Player player) : base(player) { }


    public override void Visit(DieState dieState)
    {
        return;
    }

    public override void Visit(SkillState skillState)
    {
        if(skillState.IsEnd())
        {
            BasicState basicState = player.GetBasicState();
            basicState.Stop();
            player.SetState(basicState);
        }
    }

    public override void Visit(BasicState basicState)
    {
        basicState.Stop();
    }
}
