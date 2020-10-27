using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem 
{
    private Unit unit;
    public BuffSystem(Unit unit)
    {
        this.unit = unit;
    }

    private List<Buff> buffs = new List<Buff>();
    public void Tick(float deltaTime)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].Tick(Time.deltaTime);
            if (buffs[i].IsEnded())
            {
                buffs.RemoveAt(i);
                return;
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].TypeValue == buff.TypeValue)
            {
                buffs[i].Stack();
                return;
            }
        }
        buff.SetTargetUnit(unit);
        buffs.Add(buff);
        buff.Begin();
    }
}
