using UnityEngine;

public class GameAssets : MonoBehaviour
{
	private static GameAssets instance;
	public static GameAssets GetInstance()
	{
		return instance;
	}

	private void Awake()
	{
		instance = this;
	}



	[SerializeField]
	private Material UnitMaterial;
	public Material GetUnitMaterial()
	{
		return UnitMaterial;
	}
	
	[SerializeField]
	[ColorUsage(true, true)]
	private Color pinegreenColor;
	public Color GetPinegreenColor()
	{
		return new Color();
	}

	[SerializeField]
	[ColorUsage(true, true)]
	private Color firebrickColor;
	public Color GetFirebrickColor()
	{
		return new Color();
	}

	[SerializeField]
	[ColorUsage(true, true)]
	private Color cobaltblueColor;
	public Color GetCobaltblueColor()
	{
		return cobaltblueColor;
	}
}
