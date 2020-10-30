using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : Command
{
    private Unit targetUnit;
    private IAttackStrategy attackStrategy;

    public AttackCommand(Player player, Unit targetUnit) : base(player)
    {
        this.targetUnit = targetUnit;
        attackStrategy = player.GetAttackStrategy();

    }
    public override void Visit(DieState dieState) { }



    public override void Visit(BasicState basicState)
    {
        if(attackStrategy.GetRange() < player.DistanceToUnit(targetUnit))
        {
            basicState.ChaseTarget(targetUnit);
        }
        else
        {
            basicState.AttackTarget(targetUnit);
        }            
    }

    public override void Visit(SkillState skillState)
    {
        if(skillState.IsEnd())
        {
            BasicState basicState = player.GetBasicState();
            basicState.ChaseTarget(targetUnit);
            player.SetState(basicState);            
        }
        
    }
}
