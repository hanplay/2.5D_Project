using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFXVixualizer : MonoBehaviour
{
	private Unit unit;
	private Transform modelTransform;
	private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	private void Awake()
	{
		modelTransform = transform.Find("model");
		Material material = new Material(GameAssets.GetInstance().GetUnitMaterial());
		FindSpriteRenderer(modelTransform);
		SetMaterial(material);
		unit = GetComponent<Unit>();
        unit.OnDead += Unit_OnDead;
	}

    private void Unit_OnDead(object sender, System.EventArgs e)
    {
		FlickFadeaway(5, 12);
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
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.material.SetColor("_Color", color);
		}
	}

	public void GlowOff()
	{
		Glow(Color.black);
	}

	public void Flicker(Color color)
	{
		StartCoroutine(GlowFadeInCoroutine(color, 0.15f));
		StartCoroutine(GlowFadeOutCoroutine(color, 0.15f));
	}

	public void GlowFadeIn(Color color)
	{
		StartCoroutine(GlowFadeInCoroutine(color, 0.15f));
	}

	public void GlowFadeOut(Color color)
	{
		StartCoroutine(GlowFadeOutCoroutine(color, 0.15f));
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

	public void FlickFadeaway(float duration, int count)
    {
		StartCoroutine(FlickFadeawayCoroutine(duration, count));
    }

	private IEnumerator FlickFadeawayCoroutine(float duration, int count)
	{
		float deltaDuration = duration / count;
		bool isVisible = false;
		for (int i = 0; i < count; i++)
		{
			SetSpritesVisible(isVisible);
			isVisible = isVisible ^ true; // toggle the isVisible variable
			yield return new WaitForSeconds(deltaDuration);
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
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.material = material;
		}
	}

}
