public abstract class Buff
{

	protected Unit targetUnit;
	private BuffDatum buffDatum;
	
	//만약에 isEnded가 true면 tick함수를 호출하는 Unit에서 이 buff를 제거한다.
	protected bool isEnded;
	protected int maxStack = 1;
	protected int currentStack;

	public Buff(BuffDatum buffDatum, Unit targetUnit)
	{
		this.buffDatum = buffDatum;
		this.targetUnit = targetUnit;
		maxStack = buffDatum.GetMaxStack();
	}

	public abstract void Tick(float deltaTime);

	public BuffDatum GetBuffDatum()
	{
		return buffDatum;
	}

	virtual protected void Begin()
	{
		isEnded = false;
		ApplyEffects();
	}

	protected void End()
	{
		isEnded = true;
		EraseEffects();
	}

	public bool IsEnded()
	{
		return isEnded;
	}

	public abstract void ApplyEffects();
	public abstract void EraseEffects();
	public bool CanBeStacked()
	{
		if(maxStack > currentStack)
		{
			return true;
		}
		return false;
	}

}