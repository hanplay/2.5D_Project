using System;
using UnityEngine;

[Serializable]
public class LevelSystem 
{
    public event EventHandler OnExperiencePointsChanged;
    public event EventHandler OnLevelUp;

    private int level = 1;
    private int experiencePoints;
    private int[] experiencePointsPerLevel = new int[]
    {
        0  ,
        100,// 1
        120,// 2
        140,// 3
        160,// 4
        180,// 5
        200,// 6
        220,// 7
        250,// 8
        300,// 9
        0   // 10
    };
    
    public float GetProportion()
    {
        if (IsMaxLevel())
        {
            return 0f;
        }
        return (float)experiencePoints / (float)experiencePointsPerLevel[level];
    }



    public int GetLevel()
    {
        return level;
	}

    public int GetMaxLevel()
    {
        return experiencePointsPerLevel.Length - 1;
	}

    public void AddExperiecnePoints(int addedExperiecnePoints)
    {
        while (false == IsMaxLevel() && 0 != addedExperiecnePoints)
        {
            experiencePoints += addedExperiecnePoints;
            if (experiencePoints >= experiencePointsPerLevel[level])
            {
                addedExperiecnePoints = experiencePoints - experiencePointsPerLevel[level];
                MakeLevelUp();
            }
            else
            {
                addedExperiecnePoints = 0;
            }
        }
        OnExperiencePointsChanged?.Invoke(this, EventArgs.Empty);

    }
    public void MakeLevelUp()
    {
        if (IsMaxLevel())
            return;
        level++;
        experiencePoints = 0;
    }

    public bool IsMaxLevel()
    {
        if (GetMaxLevel() == level)
            return true;
        else
            return false;
    }


}
