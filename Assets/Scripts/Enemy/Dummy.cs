public class Dummy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        statsSystem.Init(0, 0);
        healthPointsSystem.Init(100);
        moveSystem.Init(new StraightMoveStrategy(this), 0f);
    }
}
