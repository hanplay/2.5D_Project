using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_HealthPointsModule : MonoBehaviour
{
	[SerializeField]
	private Unit unit;
	private HealthPointsSystem healthPointsSystem;
	private Image healthPointsBar;
    private Animator animator;
    private CanvasRenderer canvasRenderer;

	private enum State
    {
		Increasing,
		Decreasing,
		Default
    }
    private State state;

	private float lagTime;
	private float secondPerFrame;
    private const float TOTAL_ANIMATING_TIME = 0.3f;
    private const float DELTA_HEALTH_POINTS_PROPORTION = 0.005f;


    private float healthPointsProportion;
	
	private void Awake()
	{
		healthPointsBar = transform.Find("HealthPointsBar").GetComponent<Image>();
        animator = GetComponent<Animator>();
        canvasRenderer = GetComponent<CanvasRenderer>();
		healthPointsSystem = unit.GetHealthPointsSystem();
        healthPointsSystem.OnHealthPointsChanged += HealthPointsSystem_OnHealthPointsChanged;
	}

    private void HealthPointsSystem_OnHealthPointsChanged()
    {
        if (false == gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            animator.Play("Appear");
        }
        /*한 프레임당 시간 = 에니메이션하는 시간 / 체력의 변화량  / 한 프레임당 체력의 변화량  */
        secondPerFrame = TOTAL_ANIMATING_TIME / (Mathf.Abs(healthPointsProportion - unit.GetHealthPointsSystem().GetProportion()) / DELTA_HEALTH_POINTS_PROPORTION);        
        healthPointsProportion = healthPointsSystem.GetProportion();
        lagTime = 0f;
        if(healthPointsProportion > healthPointsBar.fillAmount)
        {
            state = State.Increasing;
        }
        else
        {
            state = State.Decreasing;
        }
    }

    private void Update()
    {
        lagTime += Time.deltaTime;
        switch(state)
        {
        case State.Default:
            if (3f < lagTime)
            {
                lagTime = 0f;                
                animator.Play("Disappear");
            }
            break;                 
        case State.Increasing:
            if (healthPointsBar.fillAmount <= healthPointsProportion)
            {
                while(lagTime >= secondPerFrame)
                {
                    IncreaseHealthPoints();
                    lagTime -= secondPerFrame;
                }
            }
            else            
                state = State.Default;            
            break;
        case State.Decreasing:
            if (healthPointsBar.fillAmount >= healthPointsProportion)
            {
                while (lagTime >= secondPerFrame)
                {
                    DecreaseHealthPoints();
                    lagTime -= secondPerFrame;
                }
            }                
            else
                state = State.Default;
            break;
        }
    }

    private void ConcealAnimationEnd()
    {
        gameObject.SetActive(false);
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
