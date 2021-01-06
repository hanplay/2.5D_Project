using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave<T> where T : Unit 
{
    [SerializeField]
    private List<T> waveUnitList = new List<T>();
    private System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

    public enum State
    {
        Hide,
        Battle,
        End,
    }

    private State state = State.Battle;

    public void Init()
    {
        for (int i = 0; i < waveUnitList.Count; i++)
        {
            waveUnitList[i].GetHealthPointsSystem().OnDead += Unit_OnDead;
        }
    }
    public void Hide()
    {
        for(int i = 0; i < waveUnitList.Count; i++)
        {
            waveUnitList[i].gameObject.SetActive(false);
        }
        state = State.Hide;
    }

    public void Show()
    {
        for (int i = 0; i < waveUnitList.Count; i++)
        {
            waveUnitList[i].gameObject.SetActive(true);
        }
        state = State.Battle;
    }

    private void Unit_OnDead(Unit unit)
    {
        T typeUnit = unit as T;
        waveUnitList.Remove(typeUnit);
        if(0 == waveUnitList.Count)
        {
            state = State.End;
        }
    }

    public State GetState()
    {
        return state;
    }

    public T GetRandomUnit()
    {
        int count = waveUnitList.Count;
        if (0 == count)
            return null;
        int randomIndex = random.Next(0, waveUnitList.Count);
        return waveUnitList[randomIndex];
    }

    public List<T> GetAllUnit()
    {
        return waveUnitList;
    }

}
