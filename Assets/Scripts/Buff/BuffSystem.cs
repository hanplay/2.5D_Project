using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem 
{
    public event EventHandler OnBuffsChanged;

    private Unit unit;
    public BuffSystem(Unit unit)
    {
        this.unit = unit;
    }

    private List<Buff> buffList = new List<Buff>();
    public void Tick(float deltaTime)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].Tick(Time.deltaTime);
            if (buffList[i].IsEnded())
            {
                buffList.RemoveAt(i);
                OnBuffsChanged?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        buff = buff.Clone() as Buff;
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].TypeValue == buff.TypeValue)
            {
                buffList[i].Stack();
                return;
            }
        }
        buff.SetTargetUnit(unit);
        buffList.Add(buff);
        buff.Begin();
        OnBuffsChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Buff> GetBuffList()
    {
        return buffList;
    }
}
