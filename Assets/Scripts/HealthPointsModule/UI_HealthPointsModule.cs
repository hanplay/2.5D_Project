using UnityEngine;
using UnityEngine.UI;

public class UI_HealthPointsModule : MonoBehaviour
{
	[SerializeField]
	private Unit unit;
	private Image healthPointsBar;

	private float lagTime;
	private float secondPerFrame;
	private const float TOTAL_ANIMATING_TIME = 0.3f;
	private const float DELTA_HEALTH_POINTS_PROPORTION = 0.005f;

	private bool isAnimating;
	private bool isIncreasing;
	
	private float healthPointsProportion;
	
	private void Awake()
	{
		healthPointsBar = GetComponentInChildren<Image>();
	}
	private void Start()
	{
		unit.GetHealthPointsSystem().OnHealthPointChanged += HealthPointModule_OnHealthPointChanged;
	}

	private void HealthPointModule_OnHealthPointChanged(object sender, System.EventArgs e)
	{
		if (healthPointsProportion < unit.GetHealthPointsSystem().GetProportion())
		{
			isIncreasing = true;
		}
		else
		{
			isIncreasing = false;
		}

		secondPerFrame = TOTAL_ANIMATING_TIME / (Mathf.Abs(healthPointsProportion - unit.GetHealthPointsSystem().GetProportion()) / DELTA_HEALTH_POINTS_PROPORTION);
		healthPointsProportion = unit.GetHealthPointsSystem().GetProportion();

		isAnimating = true;
	}



	private void Update()
	{
		if (false == isAnimating)
			return;

		//handmade fixedUpdate
		lagTime += Time.deltaTime;
		while (secondPerFrame < lagTime)
		{
			lagTime -= secondPerFrame;

			if (true == isIncreasing)
			{
				if (healthPointsBar.fillAmount <= healthPointsProportion)
				{
					IncreaseHealthPoints();
				}
				else
				{
					isAnimating = false;
				}
			}
			else
			{
				if (healthPointsBar.fillAmount >= healthPointsProportion)
				{
					DecreaseHealthPoints();
				}
				else
				{
					isAnimating = false;
				}
			}
		}
	}
		
	private void IncreaseHealthPoints()
	{
		healthPointsBar.fillAmount += DELTA_HEALTH_POINTS_PROPORTION;
	}

	private void DecreaseHealthPoints()
	{
		healthPointsBar.fillAmount -= DELTA_HEALTH_POINTS_PROPORTION;
	}
}
