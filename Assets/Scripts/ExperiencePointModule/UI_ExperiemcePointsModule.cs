using UnityEngine;
using UnityEngine.UI;

public class UI_ExperiemcePointsModule : MonoBehaviour
{
	[SerializeField]
	private LevelSystem experiencePointsModule;
	private Image animatedExperiencePointsBar;

	private float lagTime;
	private const float SECOND_PER_FRAME = 0.008f;
	private const float DELTA_EXPERIENCE_POINTS_PROPORTION = 0.005f;

	private bool isAnimating = false;
	private bool isIncreasing;

	private int animatedLevel = 1;
	private float experiencePointsProportion;

	private void Awake()
	{
		animatedExperiencePointsBar = GetComponent<Image>();
	}

	private void Start()
	{
		animatedExperiencePointsBar.fillAmount = experiencePointsProportion = experiencePointsModule.GetProportion();
		experiencePointsModule.OnExperiencePointsChanged += ExperiencePointsModule_OnExperiencePointsChanged;
	}

	private void ExperiencePointsModule_OnExperiencePointsChanged(object sender, System.EventArgs e)
	{
		if (animatedLevel < experiencePointsModule.GetLevel())
		{
			experiencePointsProportion = experiencePointsModule.GetProportion();
			isIncreasing = true;
		}
		else if (animatedLevel == experiencePointsModule.GetLevel())
		{
			if (experiencePointsProportion < experiencePointsModule.GetProportion())
			{
				experiencePointsProportion = experiencePointsModule.GetProportion();
				isIncreasing = true;
			}

		}
		else
		{
			isIncreasing = false;
		}
		isAnimating = true;
	}

	private void Update()
	{
		if (false == isAnimating)
			return;

		lagTime += Time.deltaTime;
		while (lagTime > SECOND_PER_FRAME)
		{
			lagTime -= SECOND_PER_FRAME;

			if (true == isIncreasing)
			{
				if (CanAnimateIncreasing())
				{
					IncreaseExperiencePoints();
				}
				else
				{
					isAnimating = false;
				}
			}
			else
			{
				if (animatedExperiencePointsBar.fillAmount >= experiencePointsProportion)
				{
					DecreaseExperiencePoints();
				}
				else
				{
					isAnimating = false;
				}
			}

		}
	}

	private void OnEnable()
	{
		animatedExperiencePointsBar.fillAmount = experiencePointsProportion;
	}

	private void IncreaseExperiencePoints()
	{
		if (1f == animatedExperiencePointsBar.fillAmount)
		{
			animatedLevel++;
			animatedExperiencePointsBar.fillAmount = 0f;
		}
		animatedExperiencePointsBar.fillAmount += DELTA_EXPERIENCE_POINTS_PROPORTION;
	}

	private void DecreaseExperiencePoints()
	{
		animatedExperiencePointsBar.fillAmount -= DELTA_EXPERIENCE_POINTS_PROPORTION;
	}

	private bool CanAnimateIncreasing()
	{
		if (animatedLevel < experiencePointsModule.GetLevel())
		{
			return true;
		}
		else if (animatedLevel == experiencePointsModule.GetLevel())
		{
			if (animatedExperiencePointsBar.fillAmount <= experiencePointsProportion)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
}
