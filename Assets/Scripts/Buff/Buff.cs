using System;
using UnityEngine;



public abstract class Buff : ICloneable
{
	protected Unit targetUnit;
	private Sprite buffSprite;
	
	//만약에 isEnded가 true면 tick함수를 호출하는 Unit에서 이 buff를 제거한다.
	protected bool isEnded;
	protected int maxStack = 1;
	protected int currentStack;

	public const int DoNotShow = -1;

	public void SetBuffSprite(Sprite buffSprite)
    {
		this.buffSprite = buffSprite;
    }

	public void SetTargetUnit(Unit targetUnit)
    {
		this.targetUnit = targetUnit;
    }

	public abstract void Tick(float deltaTime);

	virtual public void Begin()
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

	public Sprite GetBuffSprite()
    {
		return buffSprite;
    }
    public abstract object Clone();
	public abstract int IndexNumber();
}