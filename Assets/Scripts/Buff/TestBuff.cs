using UnityEngine;

public class TestBuff : Buff
{
	public TestBuff(BuffDatum buffDatum, Unit targetUnit) : base(buffDatum, targetUnit)
	{
		
	}

	public override void ApplyEffects()
	{
		Debug.Log("Test apply Effects");
	}

	public override void EraseEffects()
	{
		Debug.Log("Test erase Effects");
	}

	public override void Tick(float deltaTime)
	{
		return;
	}
}
