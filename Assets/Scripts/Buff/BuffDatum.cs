using UnityEngine;

public abstract class BuffDatum : ScriptableObject
{
    [SerializeField]
    protected float duration;
    [SerializeField]
    private int maxStack;

	private void Awake()
	{
        maxStack = 1;
	}

	public float GetDuration()
    {
        return duration;
	}

    public int GetMaxStack()
    {
        return maxStack;
	}
}
