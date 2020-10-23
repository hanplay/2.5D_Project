using UnityEngine;

public class BuffSkillState : SkillState
{
    private Buff buff;
    private GameObject buffVisualEffect;
    public BuffSkillState(Player player, Skill skill, Buff buff, GameObject buffVisualEffect) : base(player, skill)
    {
        this.buff = buff;
        this.buffVisualEffect = buffVisualEffect;
    }

    public override void Begin()
    {
        base.Begin();
        player.SkillAction[0] = Work;
        animator.Play("Spell");
        if (0 == duration)
            duration = player.GetClipLength("Spell");
    }

    private void Work()
    {
        Debug.Log("Buff!!");       
        GameObject.Instantiate(buffVisualEffect, player.GetPosition(), Quaternion.Euler(90, 0f, 0), player.transform);        

        Buff newBuff = buff.Clone() as Buff;
        player.AddBuff(newBuff);
        newBuff.Begin();
    }

}
