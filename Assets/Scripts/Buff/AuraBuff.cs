public abstract class AuraBuff : Buff
{
    private float cycleDuration = 0.3f;
    private float lagTime;
    public AuraBuff(BuffType TypeValue, float cycleDuration) : base(TypeValue)
    {
        this.cycleDuration = cycleDuration;
    }

    public AuraBuff(BuffType TypeValue) : base(TypeValue) { }

    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void Stack()
    {
        lagTime = 0f;
    }

    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;
        if (lagTime >= cycleDuration)
        {
            End();
            lagTime = 0f;
        }
    }

}
