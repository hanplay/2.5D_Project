using System;
using UnityEngine;

public abstract class TimedBuff : Buff, ICloneable
{
	protected float duration;
	private float lagTime;

	public TimedBuff(BuffType TypeValue, float duration) : base(TypeValue)
	{
		this.duration = duration;
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
	public float GetRemainingTimeProportion()
	{
		return 1f - (lagTime / duration);
	}

}
