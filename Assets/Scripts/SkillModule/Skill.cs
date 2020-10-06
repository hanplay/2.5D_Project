using UnityEngine;

[System.Serializable]
public abstract class Skill 
{
  
    private bool isLocked;
    private bool isCooldown;

    private float cooldownTime;
    private float lagTime = 0f;

    private readonly string animationName;

    private Player player;



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
    
    
    public void TryToExecute(Animator animator)
    {        
        if(CanExecute())
        {
            Execute(animator);
            isCooldown = true;
		}
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

    public abstract bool CanExecute();

	public void Execute(Animator animator)
	{
        isCooldown = true;
	}
}