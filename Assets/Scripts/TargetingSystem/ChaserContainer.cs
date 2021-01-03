using System.Collections.Generic;

public class ChaserContainer 
{
    private List<Unit> chaserUnitList = new List<Unit>();

    public ChaserContainer(Unit owner)
    {
        owner.GetHealthPointsSystem().OnDead += ChaserContainer_OnDead;
    }

    private void ChaserContainer_OnDead(Unit owner)
    {
        NotifyUnitDead();
    }

    public void AddChaser(Unit unit)
    {
        chaserUnitList.Add(unit);
    }

    public void RemoveChaser(Unit unit)
    {
        chaserUnitList.Remove(unit);
    }

    public int GetChaserCount()
    {
        return chaserUnitList.Count;
    }

    public List<Unit> GetList()
    {
        return chaserUnitList;
    }

    private void NotifyUnitDead()
    {
        for(int i = 0; i < chaserUnitList.Count; i++)
        {
            chaserUnitList[i].GetTargetedUnitHandler().OnTargetDead();
        }
    }
}
