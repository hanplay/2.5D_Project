using System;
using UnityEngine;

public abstract class Buff : ICloneable
{
	public readonly BuffType TypeValue;
	protected Unit owner;
	protected Sprite buffSprite;
	
	//만약에 isEnded가 true면 tick함수를 호출하는 Unit에서 이 buff를 제거한다.
	protected bool isEnded;
	protected int maxStack = 1;
	protected int currentStack = 1;
	
	public const int DoNotShow = -1;

    protected Buff(BuffType TypeValue)
    {
		this.TypeValue = TypeValue;
    }

    public void SetBuffSprite(Sprite buffSprite)
    {
		this.buffSprite = buffSprite;
    }

	public virtual void SetTargetUnit(Unit targetUnit)
    {
		this.owner = targetUnit;
    }

	public abstract void Tick(float deltaTime);

	virtual public void Begin()
	{ 
		ApplyEffects();
	}

	public void End()
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
    public object Clone()
    {
		return GameAssets.Instance.CreateBuff(TypeValue);
    }
	public abstract int IndexNumber();
	public abstract void Stack();
}