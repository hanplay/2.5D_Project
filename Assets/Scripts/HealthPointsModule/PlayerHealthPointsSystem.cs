using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPointsSystem : HealthPointsSystem
{
    private int addedMaxHealthPointsPerLevelUp;
    private LevelSystem levelSystem;
    public PlayerHealthPointsSystem(int baseMaxHealthPoints, int addedMaxHealthPointsPerLevelUp, LevelSystem levelSystem)
    {
        this.baseMaxHealthPoints = baseMaxHealthPoints;
        this.levelSystem = levelSystem;
        this.addedMaxHealthPointsPerLevelUp = addedMaxHealthPointsPerLevelUp;
        CalculateBaseHealthPointsAccordingToLevel();
        levelSystem.OnLevelUp += LevelSystem_OnLevelUp;
        FillHealthPointsFull();
    }

    private void LevelSystem_OnLevelUp(object sender, System.EventArgs e)
    {
        CalculateBaseHealthPointsAccordingToLevel();
        FillHealthPointsFull();
    }

    private void CalculateBaseHealthPointsAccordingToLevel()
    {
        int levelMinusOne = levelSystem.GetLevel() - 1;
        baseMaxHealthPoints = baseMaxHealthPoints + addedMaxHealthPointsPerLevelUp * levelMinusOne;
        CalculateMaxHealthPoints();
    }
}
