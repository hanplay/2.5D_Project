
public abstract class TimedBuff : Buff
{
	private float duration;
	private float lagTime;
	
	public TimedBuff(BuffDatum buffDatum, Unit targetUnit) : base(buffDatum, targetUnit)
	{
		Begin();
	}

	public override void Tick(float deltaTime)
	{
		lagTime += deltaTime;
		if(lagTime >= duration)
		{
			End();
			lagTime = 0f;
		}
	}

	protected override void Begin()
	{
		base.Begin();
	}
}
