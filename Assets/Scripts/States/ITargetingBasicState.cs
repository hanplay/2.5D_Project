public interface ITargetingBasicState 
{
    void ActivateTargetingSkill(Skill targetingSkill);
    void SetTarget(Unit targetedUnit);
    void ReleaseTarget();
}
