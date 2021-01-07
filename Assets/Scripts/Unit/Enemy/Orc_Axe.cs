public class Orc_Axe : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        statsSystem.Init(15, 0);
        healthPointsSystem.Init(300);
        moveSystem.Init(new StraightMoveStrategy(this), 4f);
        attackStrategy = new InstantAttackStrategy(this, new TrueDamageStrategy(), 2f);
        stateSystem.PushState(new SummonState(this, 3f));
    }
}
