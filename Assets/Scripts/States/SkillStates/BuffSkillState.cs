using UnityEngine;

public class BuffSkillState : SkillState
{
    private Buff buff;
    private GameObject buffVisualEffect;
    public BuffSkillState(Unit player, Skill skill, Buff buff, GameObject buffVisualEffect) : base(player, player.GetStateSystem(), skill)
    {
        this.buff = buff;
        this.buffVisualEffect = buffVisualEffect;
    }
    public BuffSkillState(Unit player, Skill skill, Buff buff) : base(player, player.GetStateSystem(), skill)
    {
        this.buff = buff;
        buffVisualEffect = null;
    }

    public override void Begin()
    {
        base.Begin();
        Player player = owner as Player;
        player.GetSkillSystem().SkillAction = Work;
        animator.Play("Spell");
        if (0 == duration)
            duration = player.GetClipLength("Spell");
    }

    private void Work()
    {
        if(null != buffVisualEffect)
            GameObject.Instantiate(buffVisualEffect, owner.GetPosition(), Quaternion.Euler(90f, 0f, 0), owner.transform);        

        Buff newBuff = buff.Clone() as Buff;
        owner.GetBuffSystem().AddBuff(newBuff);
    }

    public override bool IsTargetingState()
    {
        return false;
    }
}
