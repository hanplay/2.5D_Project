using UnityEngine;

public class TargetSkillCommmand : SkillCommand
{
    
    private float range = 5f;

    public TargetSkillCommmand(Player player, Skill skill) : base(player, skill)
    {
        this.player = player;
        skill.SetChase(true);
        
    }



    public override void Visit(SkillState skillState)
    {
        //if(skillState.GetTargetUnit() == targetUnit && range >= player.DistanceToUnit(targetUnit))
        //{
        //    skillState.ChageToSkillState(skillState);
        //}
    }

    public override void Visit(BasicState basicState)
    {   
        if(false == basicState.IsTargetingState())
        {
            BasicState idleState = player.GetBasicState();
            idleState.Stop();
            player.SetState(idleState);
        }

        Unit targetUnit = basicState.GetTargetUnit();
        if(range > player.DistanceToUnit(targetUnit))
        {
            SkillState skillState = skill.GetSkillState();
            skillState.SetTargetUnit(targetUnit);
            basicState.ChageToSkillState(skill.GetSkillState());
        }
        else
        {
            basicState.ChaseTarget(targetUnit);
        }
        
    }
}
