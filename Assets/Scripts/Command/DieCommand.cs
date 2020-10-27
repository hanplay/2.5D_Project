using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCommand : Command
{
    public DieCommand(Player player) : base(player) { }



    public override void Visit(DieState dieState)
    {
        return;
    }

    public override void Visit(SkillState skillState)
    {
        skillState.ChangeToDieState();
    }

    public override void Visit(BasicState basicState)
    {
        basicState.ChangeToDieState();
    }
}
