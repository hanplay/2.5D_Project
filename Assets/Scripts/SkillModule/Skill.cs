﻿using UnityEngine;

public class Skill 
{
    private Player player;
    private Sprite skillSprite;

    private SkillState skillState;

    private bool canCancel;
    private bool isTargetSkill;

    private float cooldownTime;

    private float lagTime = 0f;
    private bool isCooldownTime;

    public Skill(Player player) 
    {
        this.player = player;
    }

    public void SetCanCancel(bool canCancel)
    {
        this.canCancel = canCancel;
    }

    public void SetIsTargetSkill(bool isTargetSkill)
    {
        this.isTargetSkill = isTargetSkill;
    }

    public void SetCooldownTime(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }

    public void SetSkillSprite(Sprite skillSprite)
    {
        this.skillSprite = skillSprite;
    }

    public void SetSkillState(SkillState skillState)
    {
        this.skillState = skillState;
    }


    public void Tick(float deltaTime)
    {
        if(isCooldownTime)
        {
            lagTime += deltaTime;
            if(lagTime <= cooldownTime)
            {
                lagTime = 0f;
                isCooldownTime = false;
			}
		}           
	}

	public float GetRemainingCoolDownTimeProportion()
    {
        return 1f - lagTime / cooldownTime;
    }

    public SkillState GetSkillState()
    {
        return skillState;
    }
    public void StartCooldownTime()
    {
        isCooldownTime = true;
    }
    public bool IsCoolDownTime()
    {
        return isCooldownTime;
    }


    public bool CanCancel()
    {
        return canCancel;
    }
    public bool IsTargetSkill()
    {
        return isTargetSkill;
    }

    public Sprite GetSkillSprite()
    {
        return skillSprite;
    }

    public void OrderPlayerTargetSkillCommand()
    {
        if (null == player)
        {
            Debug.Log("player is null");
        }

        if (null == player.GetState())
        {
            Debug.Log("State is null");
        }


        if (null == player.GetState().GetTargetUnit())
        {
            Debug.Log("target unit is null");
        }
        player.SetCommand(new TargetSkillCommmand(player, this, player.GetState().GetTargetUnit()));
    }

    public void OrderPlayerBasicSkillCommand()
    {
        player.SetCommand(new BasicSkillCommand(player, this));        
    }
}