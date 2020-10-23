using UnityEngine;

public class TestBuff : TimedBuff
{
	public TestBuff(float duration) : base(duration) 
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

    public override object Clone()
    {
		return new TestBuff(duration);
    }

    public override int IndexNumber()
    {
        return 34;
    }
}
