using UnityEngine;
using System;

public class HealthPointsSystem 
{
    public delegate void UnitDeadEventHandler(Unit owner);
    public delegate void HealthPointsChangeEventHandler();
    public event HealthPointsChangeEventHandler OnHealthPointsChanged;
    public event UnitDeadEventHandler OnDead;
    //public event EventHandler OnHealthPointsChanged;

    private Unit owner;
    private int baseMaxHealthPoints;
    private int addedMaxHealthPoints;
    private int totalMaxHealthPoints;
    private int healthPoints;

    protected HealthPointsSystem() { }
    public HealthPointsSystem(Unit unit) 
    {
        owner = unit;
    }

    public void Init(int baseMaxHealthPoints)
    {
        this.baseMaxHealthPoints = baseMaxHealthPoints;
        CalculateMaxHealthPoints();
        FillHealthPointsFull();
    }



    public void AddHealthPoints(int points)
    {
        healthPoints += points;
        if(totalMaxHealthPoints < healthPoints)
        {
            healthPoints = totalMaxHealthPoints;
		}
        OnHealthPointsChanged.Invoke();
	}
    public void SubtractHealthPoints(int points)
    {
        healthPoints -= points;
        if(0 > healthPoints)
        {
            healthPoints = 0;
		}
        OnHealthPointsChanged.Invoke();

        if (0 == healthPoints)
        {
            OnDead?.Invoke(owner);
            OnDead.GetInvocationList().Initialize();
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
        OnHealthPointsChanged?.Invoke();
    }

    public void CalculateMaxHealthPoints()
    {
        totalMaxHealthPoints = baseMaxHealthPoints + addedMaxHealthPoints;
        OnHealthPointsChanged?.Invoke();
    }

}
