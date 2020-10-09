using UnityEngine;

[System.Serializable]
public abstract class Skill 
{
    private SkillState skillState;

    private bool isLocked;
    private bool isCooldown;

    private float cooldownTime;
    private float lagTime = 0f;

    private float range;

    public void Tick(float deltaTime)
    {
        if(isCooldown)
        {
            lagTime += deltaTime;
            if(lagTime <= cooldownTime)
            {
                lagTime = 0f;
                isCooldown = false;
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
    

    public bool IsLocked()
    {
        return isLocked;        
	}

    public bool IsCooldown()
    {
        return isCooldown;
	}

    public void Unlock()
    {
        isLocked = false;
	}

    public void Lock()
    {
        isLocked = true;
	}

    public abstract int GetIndexNumber();

}