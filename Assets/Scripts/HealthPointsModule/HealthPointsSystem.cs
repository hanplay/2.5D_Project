using System;

public class HealthPointsSystem 
{
    public delegate void HealthPointsChangeEventHandler(float proportion);
    public event HealthPointsChangeEventHandler OnHealthPointsChanged;
    //public event EventHandler OnHealthPointsChanged;

    protected int baseMaxHealthPoints;
    protected int addedMaxHealthPoints;
    protected int totalMaxHealthPoints;
    protected int healthPoints;

    protected HealthPointsSystem() { }
    public HealthPointsSystem(Unit unit) 
    {

    }

    public void Init(int baseMaxHealthPoints)
    {
        this.baseMaxHealthPoints = baseMaxHealthPoints;
    }




    public void AddHealthPoints(int points)
    {
        healthPoints += points;
        if(totalMaxHealthPoints < healthPoints)
        {
            healthPoints = totalMaxHealthPoints;
		}
        OnHealthPointsChanged.Invoke(GetProportion());
	}
    public void SubtractHealthPoints(int points)
    {
        healthPoints -= points;
        if(0 > healthPoints)
        {
            healthPoints = 0;
		}
        OnHealthPointsChanged.Invoke(GetProportion());
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
        OnHealthPointsChanged.Invoke(GetProportion());
    }

    public void CalculateMaxHealthPoints()
    {
        totalMaxHealthPoints = baseMaxHealthPoints + addedMaxHealthPoints;
        OnHealthPointsChanged.Invoke(GetProportion());
    }

}
