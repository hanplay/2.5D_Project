using System;

public class HealthPointsSystem 
{
    public event EventHandler OnHealthPointChanged;
    public event EventHandler OnDead;

    protected int baseMaxHealthPoints;
    protected int addedMaxHealthPoints;
    protected int totalMaxHealthPoints;
    protected int healthPoints;

    protected HealthPointsSystem() { }
    public HealthPointsSystem(int baseMaxHealthPoints) 
    {
        this.baseMaxHealthPoints = baseMaxHealthPoints;
        FillHealthPointsFull();
    }


    public void AddHealthPoints(int points)
    {
        healthPoints += points;
        if(totalMaxHealthPoints < healthPoints)
        {
            healthPoints = totalMaxHealthPoints;
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
        return totalMaxHealthPoints;
	}
    public int GetHealthPoints()
    {
        return healthPoints;
	}

	public float GetProportion()
    {
        return (float)healthPoints / (float)totalMaxHealthPoints;
	}

    public void FillHealthPointsFull()
    {
        healthPoints = totalMaxHealthPoints;
	}

    public void CalculateMaxHealthPoints()
    {
        totalMaxHealthPoints = baseMaxHealthPoints + addedMaxHealthPoints;
    }

}
