using System;

public class HealthPointsSystem 
{
    private EquipmentSystem equipmentSystem;
    private LevelSystem levelSystem;

    public event EventHandler OnHealthPointChanged;

    private int baseMaxHealthPoints;
    private int addedMaxHealthPointsPerLevelUp;
    private int addedMaxHealthPoints;
    private int maxHealthPoints = 100;
    private int healthPoints = 50;

    //Player
    public HealthPointsSystem(StatsDatum statsDatum, EquipmentSystem equipmentSystem, LevelSystem levelSystem)
    {
        baseMaxHealthPoints = statsDatum.GetBaseMaxHealthPoints();
        addedMaxHealthPointsPerLevelUp = statsDatum.GetAddedMaxHealthPointsPerLevelUp();

        this.equipmentSystem = equipmentSystem;
        this.levelSystem = levelSystem;
        CalculateMaxHealthPoints();
        FillHealthPointsFull();
	}

    //Enemy (Monster) or common unit
    public HealthPointsSystem(int maxHealthPoints) 
    {
        this.maxHealthPoints = maxHealthPoints;
        FillHealthPointsFull();
    }

    public int GetHealthPoints()
    {
        return healthPoints;
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

    private void CalculateMaxHealthPoints()
    {
        maxHealthPoints = baseMaxHealthPoints + addedMaxHealthPoints;
	}
}
