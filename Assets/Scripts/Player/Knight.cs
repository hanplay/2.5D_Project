using UnityEngine;

public class Knight : Player
{
	//private Transform 
	protected void Awake()
	{
		base.Awake();
	}

	protected void Update()
    {
		base.Update();

	}
	public override bool IsTargetable(Unit unit)
	{
		if (null == unit)
			return false;

		if(null != unit.GetComponent<Enemy>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}


}
