using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraBounds : MonoBehaviour
{
    private Unit unit;
    private Buff buff;
    private float period;

    private List<Unit> targetUnitList = new List<Unit>();
    private List<float> targetUnitTimerList = new List<float>();

    


    private void Update()
    {
        for(int i = 0; i < targetUnitList.Count; i++)
        {
            targetUnitTimerList[i] += Time.deltaTime;    
            if(period < targetUnitTimerList[i])
            {
                targetUnitTimerList[i] = 0f;
                PeriodicImpactOn(targetUnitList[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        if (null == targetUnit)
            return;
        if(unit.IsTargetable(targetUnit))
        {            
            Buff newBuff = buff.Clone() as Buff;
            targetUnitList.Add(targetUnit);
            targetUnit.AddBuff(newBuff);
            targetUnit.OnDead += TargetUnit_OnDead;
        }       
    }

    private void OnTriggerExit(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        if (null == targetUnit)
            return;
        if (false == unit.IsTargetable(targetUnit))
            return;

        SearchAndRemoveInList(targetUnit);
    }

    private void TargetUnit_OnDead(object sender, System.EventArgs e)
    {
        Unit targetUnit = sender as Unit;
        SearchAndRemoveInList(targetUnit);
    }


    private void SearchAndRemoveInList(Unit targetUnit)
    {
        for (int i = 0; i < targetUnitList.Count; i++)
        {
            if (targetUnitList[i] == targetUnit)
            {
                targetUnitList.RemoveAt(i);
                targetUnitTimerList.RemoveAt(i);
                targetUnit.OnDead -= TargetUnit_OnDead;
                return;
            }
        }
    }

    protected abstract void PeriodicImpactOn(Unit targetUnit);    
}
