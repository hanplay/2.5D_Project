public class Orc_Spell : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        statsSystem.Init(15, 0);
        healthPointsSystem.Init(200);
        stateSystem.PushState(new SummonState(this, 3f));
        moveSystem.Init(new StraightMoveStrategy(this), 3f);

    }

    private void Start()
    {
        attackStrategy = new InstantAttackStrategy(this, 
            new CompositeDamageStrategy(new CommonDamageStrategy(), BuffType.None, GetTargetingStrategy(), GameAssets.Instance.magicSmoke, 2f), 5f);

    }
}
