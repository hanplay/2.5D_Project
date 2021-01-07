public interface ITargetingStrategy
{
	bool IsTargetable(Unit unit);
}

public class TargetingStrategy<T> : ITargetingStrategy  where T : Unit 
{
	public bool IsTargetable(Unit unit)
	{
		if (null == unit)
			return false;

		if (null != unit as T)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
