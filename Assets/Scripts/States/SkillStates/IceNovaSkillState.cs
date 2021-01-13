using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceNovaSkillState : SkillState
{
    private Buff slowDebuff;
    private TrueDamageStrategy trueDamageStrategy = new TrueDamageStrategy();
    public IceNovaSkillState(Unit player, Skill skill, Buff buff) : base(player, player.GetStateSystem(), skill)
    {
        slowDebuff = buff;
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
        List<Enemy> enemies = BattleSystem.Instance.GetEnemyWave().GetAllUnit();
        for (int i = 0; i < enemies.Count; i++)
        {
            trueDamageStrategy.Do(enemies[i], 30);
            enemies[i].GetBuffSystem().AddBuff(slowDebuff);
        }
        Blinder.Instance.Paint(Color.white);
        Blinder.Instance.FadeOut(1f);
    }
}