using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave<T> where T : Unit 
{
    [SerializeField]
    private List<T> waveUnitList = new List<T>();
    private System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

    public void Init()
    {
        for (int i = 0; i < waveUnitList.Count; i++)
        {
            waveUnitList[i].GetHealthPointsSystem().OnDead += Unit_OnDead;
        }
    }

    private void Unit_OnDead(Unit unit)
    {
        T typeUnit = unit as T;
        waveUnitList.Remove(typeUnit);
    }

    public bool IsDead()
    {
        if (0 == waveUnitList.Count)
            return true;
        else
            return false;
    }

    public T GetRandomUnit()
    {
        int randomIndex = random.Next(0, waveUnitList.Count);
        return waveUnitList[randomIndex];
    }

    public List<T> GetAllUnit()
    {
        return waveUnitList;
    }
}
