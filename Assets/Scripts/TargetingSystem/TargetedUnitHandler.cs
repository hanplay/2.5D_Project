public class TargetedUnitHandler 
{
    private Unit owner;
    private Unit targetedUnit;
    private float closeDistance = 1.5f;


    public TargetedUnitHandler(Unit owner)
    {
        this.owner = owner;
    }

    public bool TargetInProperRange(out bool isTooClose)
    {
        isTooClose = false;
        float distance = owner.DistanceToUnit(targetedUnit);
        if (distance > owner.GetAttackStrategy().GetRange())
            return false;
        else if (distance < closeDistance)
        {
            isTooClose = true;
            return false;
        }
        else
            return true;
    }


    public bool TargetInSkillRange(Skill targetingSkill)
    {
        if (owner.DistanceToUnit(targetedUnit) > targetingSkill.GetRange())
            return false;
        else
            return true;        
    }

    public void SetTarget(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;
        targetedUnit.GetChaserContainer().AddChaser(owner);
    }

    public void ReleaseTarget()
    {
        targetedUnit.GetChaserContainer().RemoveChaser(targetedUnit);
        targetedUnit = null;
    }

    public void OnTargetDead()
    {
        targetedUnit = null;
    }

    public bool TryGetTargetedUnit(out Unit targetedUnit)
    {
        if (null == this.targetedUnit)
        {
            targetedUnit = null;
            return false;
        }
        else
        {
            targetedUnit = this.targetedUnit;
            return true;
        }
    }

    public Unit GetTargetedUnit()
    {
        return targetedUnit;
    }

}
