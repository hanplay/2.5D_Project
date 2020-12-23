using UnityEngine;

public class AuraBufferBuff : TimedBuff
{
	private float lagTime;
	private float checkCycle = 0.25f;
	private float radius;
	private ITargetingStrategy targetingStrategy;
	private GameObject auraEffect;
	private GameObject instantiatedAuraEffect;
	private Buff buff;

	public AuraBufferBuff(BuffType TypeValue, Buff buff, ITargetingStrategy targetingStrategy, float duration, GameObject auraEffect, float radius) : 
		base(TypeValue, duration) 
	{
		this.buff = buff;
		this.targetingStrategy = targetingStrategy;
		this.auraEffect = auraEffect;
		this.radius = radius;
	}
	public AuraBufferBuff(BuffType TypeValue, Buff buff, ITargetingStrategy targetingStrategy, float duration, GameObject auraEffect, float radius, float checkCycle) : 
		base(TypeValue, duration)
    {
		this.buff = buff;
		this.targetingStrategy = targetingStrategy;
		this.auraEffect = auraEffect;
		this.radius = radius;
		this.checkCycle = checkCycle;
    }


    public override void ApplyEffects()
	{
		instantiatedAuraEffect = GameObject.Instantiate(auraEffect, owner.GetPosition(), Quaternion.Euler(90f, 0f, 0), owner.transform);
		return;
	}

	public override void EraseEffects()
	{
		GameObject.Destroy(instantiatedAuraEffect);
		return;
	}


    public override void Tick(float deltaTime)
    {
		base.Tick(deltaTime);
		lagTime += deltaTime;
		if (checkCycle > lagTime)
			return;
		lagTime -= checkCycle;      			
		Collider[] colliders = Physics.OverlapSphere(owner.GetPosition(), radius);
		for(int i = 0; i < colliders.Length; i++)
        {			
			if(colliders[i].TryGetComponent<Unit>(out Unit buffedUnit))
            {
				if (false == targetingStrategy.IsTargetable(buffedUnit))
					break;
				buffedUnit.GetBuffSystem().AddBuff(buff);
            }			
        }        
    }
    public override int IndexNumber()
    {
		return DoNotShow;
    }
}
