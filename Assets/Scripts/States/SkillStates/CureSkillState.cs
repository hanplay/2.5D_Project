using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureSkillState : SkillState
{
    private GameObject cureVisualEffect;
    public CureSkillState(Unit player, Skill skill, GameObject cureVisualEffect) : base(player, player.GetStateSystem(), skill) 
    {
        this.cureVisualEffect = cureVisualEffect;
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
        List<Player> players = BattleSystem.Instance.GetPlayerWave().GetAllUnit();
        for(int i = 0; i < players.Count; i++)
        {
            int quarterHealthPoints = players[i].GetHealthPointsSystem().GetMaxHealthPoints() / 4;
            players[i].GetHealthPointsSystem().AddHealthPoints(quarterHealthPoints);
            GameObject.Instantiate(cureVisualEffect, players[i].GetPosition(), Quaternion.Euler(90f, 0f, 0), players[i].transform);
        }
    }
}
