using System;

public class HealthPointsSystem 
{
    public event EventHandler OnHealthPointChanged;
    public event EventHandler OnDead;

    private int maxHealthPoints = 100;
    private int healthPoints = 50;

    public HealthPointsSystem(int maxHealthPoints) 
    {
        this.maxHealthPoints = maxHealthPoints;
        FillHealthPointsFull();
    }


    public void AddHealthPoints(int points)
    {
        healthPoints += points;
        if(maxHealthPoints < healthPoints)
        {
            healthPoints = maxHealthPoints;
		}
	}
    public void SubtractHealthPoints(int points)
    {
        healthPoints -= points;
        if(0 > healthPoints)
        {
            healthPoints = 0;
		}
	}

    public int GetMaxHealthPoints()
    {
        return maxHealthPoints;
	}
    public int GetHealthPoints()
    {
        return healthPoints;
	}

    public void SetMaxHealthPoints(int maxHealthPoints)
    {
        this.maxHealthPoints = maxHealthPoints;
	}

	public float GetProportion()
    {
        return (float)healthPoints / (float)maxHealthPoints;
	}

    public void FillHealthPointsFull()
    {
        healthPoints = maxHealthPoints;
	}

}
