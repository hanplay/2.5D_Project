public class Bishop : Player
{
	protected void Awake()
	{
		base.Awake();
	}

	public override bool IsTargetable(Unit unit)
	{
		if (null == unit)
			return false;

		if (null != unit.GetComponent<Player>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
