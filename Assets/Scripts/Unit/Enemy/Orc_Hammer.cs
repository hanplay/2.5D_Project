public class Orc_Hammer : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        statsSystem.Init(15, 0);
        healthPointsSystem.Init(200);
        moveSystem.Init(new StraightMoveStrategy(this), 3f);
    }

    private void Start()
    {
        attackStrategy = new InstantAttackStrategy(this, new CommonDamageStrategy(), 2f);
        stateSystem.PushState(new SummonState(this, 3f));

    }
}
