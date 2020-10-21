﻿using UnityEngine;

public class Knight : Player
{
	//private Transform 
	protected void Awake()
	{
		base.Awake();
	}
	
	protected void Start()
    {
		skillList[0] = skillData.CreateSkill(this, SkillType.Dive);
		skillList[1] = null;
		skillList[2] = null;
		skillList[3] = null;
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
			transform.Find("model").GetComponent<Animator>().Play("Pierce");
		}
		if (Input.GetKeyDown(KeyCode.D))
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
