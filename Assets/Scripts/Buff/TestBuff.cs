using UnityEngine;

public class TestBuff : TimedBuff
{
	public TestBuff(BuffType TypeValue, float duration) : base(TypeValue, duration) 
    {
        this.duration = duration;
    }


    public override void ApplyEffects()
	{
		Debug.Log("Test apply Effects");
	}


    public override void EraseEffects()
	{
		Debug.Log("Test erase Effects");
	}


    public override int IndexNumber()
    {
        return 34;
    }
}
