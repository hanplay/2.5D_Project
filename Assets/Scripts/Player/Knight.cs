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
		if(Input.GetKeyDown(KeyCode.A))
        {
			transform.Find("model").GetComponent<Animator>().Play("Attack");
        }
		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.Find("model").GetComponent<Animator>().Play("Sit");
		}
		if(Input.GetKeyDown(KeyCode.D))
        {
			transform.Find("model").GetComponent<Animator>().Play("Run");
		}
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
