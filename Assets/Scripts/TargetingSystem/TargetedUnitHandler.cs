public class TargetedUnitHandler 
{
    private Unit owner;
    private Unit targetedUnit;

    public TargetedUnitHandler(Unit owner)
    {
        this.owner = owner;
    }

    public bool TargetInAttackRange()
    {
        if (owner.DistanceToUnit(targetedUnit) > owner.GetAttackStrategy().GetRange())
            return false;
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
