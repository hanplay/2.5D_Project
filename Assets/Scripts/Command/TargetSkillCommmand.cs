using UnityEngine;

public class TargetSkillCommmand : SkillCommand
{
    private Unit targetUnit;
    private float range = 5f;

    public TargetSkillCommmand(Player player, Skill skill, Unit targetUnit) : base(player, skill)
    {
        this.player = player;
        this.targetUnit = targetUnit;
        skill.SetChase(true);
        
    }

    public override void Visit(IdleState idleState)
    {
        if (range < player.DistanceToUnit(targetUnit))
        {
            idleState.ChangeToMoveState();
        }
        else
        {
            skill.GetSkillState().SetTargetUnit(targetUnit);
            idleState.ChageToSkillState(skill.GetSkillState());
        }
    }

    public override void Visit(MoveState moveState)
    {
        if (range < player.DistanceToUnit(targetUnit))
        {
            moveState.ChanegeToChaseState(targetUnit);
        }
        else
        {
            skill.GetSkillState().SetTargetUnit(targetUnit);
            moveState.ChageToSkillState(skill.GetSkillState());
        }
    }
    public override void Visit(ChaseState chaseState)
    {
        if (range < player.DistanceToUnit(targetUnit))
            chaseState.MoveToTargetUnit();
        else
        {
            skill.GetSkillState().SetTargetUnit(targetUnit);
            chaseState.ChageToSkillState(skill.GetSkillState());
        }
    }

    public override void Visit(AttackState attackState)
    {
        if (range < player.DistanceToUnit(targetUnit))
        {
            attackState.ChangeToMoveState();
        }
        else
        {
            skill.GetSkillState().SetTargetUnit(targetUnit);
            attackState.ChageToSkillState(skill.GetSkillState());
        }
    }

    public override void Visit(SkillState skillState)
    {
        //if(skillState.GetTargetUnit() == targetUnit && range >= player.DistanceToUnit(targetUnit))
        //{
        //    skillState.ChageToSkillState(skillState);
        //}
    }

}
