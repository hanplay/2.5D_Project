using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFXVisualizer : MonoBehaviour
{
	private Material material;
	private Transform modelTransform;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	private void Awake()
	{
		modelTransform = transform.Find("model");
		FindSpriteRenderer(modelTransform);
	}

	private void Start()
    {
		material = new Material(GameAssets.Instance.UnitMaterial);
		SetMaterial(material);        
    }
	
    private SpriteRenderer FindSpriteRenderer(Transform rootTransform)
	{
		foreach (Transform childTransform in rootTransform)
		{
			SpriteRenderer childSpriteRenderer = FindSpriteRenderer(childTransform);
			if (null != childSpriteRenderer)
			{
				spriteRenderers.Add(childSpriteRenderer);
			}
		}
		return rootTransform.GetComponent<SpriteRenderer>();
	}

	public void Paint(Color color)
	{
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.color = color;
		}
	}

	public void Glow(Color color)
	{
		material.SetColor("_Color", color);
	}

	public void GlowOff()
	{
		Glow(Color.black);
	}

	public void GlowFadeIn(Color color)
	{
		StartCoroutine(GlowFadeInCoroutine(color, 0.2f));
	}

	public void GlowFadeOut(Color color)
	{
		StartCoroutine(GlowFadeOutCoroutine(color, 0.2f));
	}


	private IEnumerator GlowFadeInCoroutine(Color color, float duration)
	{
		float secondPerFrame = 0.05f;
		int frames = (int)(duration / secondPerFrame);
		Color deltaColor = color / frames;
		Color flickeringColor = Color.black;
		for (int i = 0; i < frames; i++)
		{
			flickeringColor += deltaColor;
			Glow(flickeringColor);
			yield return new WaitForSeconds(secondPerFrame);

		}
	}

	private IEnumerator GlowFadeOutCoroutine(Color color, float duration)
	{
		float secondPerFrame = 0.05f;
		int frames = (int)(duration / secondPerFrame);
		Color deltaColor = color / frames;
		Color flickeringColor = Color.black;
		for (int i = 0; i < frames; i++)
		{
			flickeringColor -= deltaColor;
			Glow(flickeringColor);
			yield return new WaitForSeconds(secondPerFrame);
		}
	}

	public void SetSpritesVisible(bool value)
	{
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.enabled = value;
		}
	}
	private void SetMaterial(Material material)
	{
		if(null == material)
        {
			Debug.Log("Material is null");
        }
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.material = material;
		}
	}

	public void Dissolve(Color color, float duration)
    {
		material.SetColor("_Color", color);
		StartCoroutine(DissolveCorutine(duration));
		//material.SetInt("_Paint", 1);
	}

	public void Summon(Color color, float duration)
    {
		material.SetColor("_Color", color);
		StartCoroutine(SummonCorutine(duration));
	}

	private IEnumerator DissolveCorutine(float duration)
    {
		material.SetInt("_Paint", 0);
		float secondPerFrame = 0.05f;
		int frames = (int)(duration / secondPerFrame);
		float deltaProportion = 1f / frames;
		float fade = 1f;
		for (int i = 0; i < frames; i++)
		{
			fade -= deltaProportion;
			material.SetFloat("_Fade", fade);
			yield return new WaitForSeconds(secondPerFrame);
		}
		material.SetInt("_Paint", 1);
		material.SetColor("_Color", Color.black);
	}

	private IEnumerator SummonCorutine(float duration)
	{
		material.SetInt("_Paint", 0);
		float secondPerFrame = 0.05f;
		int frames = (int)(duration / secondPerFrame);
		float deltaProportion = 1f / frames;
		float fade = 0f;
		for (int i = 0; i < frames; i++)
		{
			fade += deltaProportion;
			material.SetFloat("_Fade", fade);
			yield return new WaitForSeconds(secondPerFrame);
		}
		material.SetInt("_Paint", 1);
		material.SetColor("_Color", Color.black);
	}
}
