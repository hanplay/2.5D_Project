﻿using UnityEngine;

public class Skill 
{
    private Sprite skillSprite;
    private SkillState skillState;

    private bool canCancel;
    private bool isTargetSkill;
    private bool isChasing;

    private float cooldownTime;

    private float lagTime = 0f;
    private bool isCooldownTime;

    private float range;

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
    public void SetChase(bool isChasing)
    {
        this.isChasing = isChasing;
    }

    public bool IsChasing()
    {
        return isChasing;
    }


    public void Tick(float deltaTime)
    {
        if(isCooldownTime)
        {
            lagTime += deltaTime;
            if(lagTime >= cooldownTime)
            {
                lagTime = 0f;
                isCooldownTime = false;
                skillState.Initialize();
			}
		}           
	}

	public float GetRemainingCoolDownTimeProportion()
    {
        if (IsCoolDownTime())
            return 1f - (lagTime / cooldownTime);
        else
            return 0f;
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

    public bool IsTargetSkill()
    {
        return isTargetSkill;
	}
    public bool CanCancel()
    {
        return canCancel;
    }

    public Sprite GetSkillSprite()
    {
        return skillSprite;
    }

    public float GetRange()
    {
        return range;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
}