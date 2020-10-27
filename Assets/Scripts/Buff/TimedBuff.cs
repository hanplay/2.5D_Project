using System;
using UnityEngine;

public abstract class TimedBuff : Buff, ICloneable
{
	public delegate void EventLagTimeChange(float proportion);
	public event EventLagTimeChange OnLagTimeChange;

	protected float duration;
	private float lagTime;

	public TimedBuff(BuffType TypeValue, float duration) : base(TypeValue)
	{
		this.duration = duration;
	}


    public override void Tick(float deltaTime)
	{
		OnLagTimeChange?.Invoke(GetRemainingTimeProportion());
		lagTime += deltaTime;
		if(lagTime >= duration)
		{
			End();
			lagTime = 0f;
		}
	}

	private float GetRemainingTimeProportion()
	{
		return 1f - (lagTime / duration);
	}

}
