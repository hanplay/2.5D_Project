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

	private enum State
    {
		Default,
		Increasing,
		Decreasing,
    }
    private State state = State.Default;

	private float lagTime;
    private const float SECOND_PER_FRAME = 0.002f;
    private const float DELTA_HEALTH_POINTS_PROPORTION = 0.0008f;


    private float healthPointsProportion;
	

    private void Awake()
    {
		healthPointsBar = transform.Find("HealthPointsBar").GetComponent<Image>();
        animator = GetComponent<Animator>();
		healthPointsSystem = unit.GetHealthPointsSystem();
        healthPointsSystem.OnHealthPointsChanged += HealthPointsSystem_OnHealthPointsChanged;
        healthPointsBar.fillAmount = healthPointsProportion = healthPointsSystem.GetProportion();        
    }


    private void HealthPointsSystem_OnHealthPointsChanged()
    {
        if (false == gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            animator.Play("Appear");
        }       
        healthPointsProportion = healthPointsSystem.GetProportion();
        lagTime = 0f;
        if(healthPointsProportion > healthPointsBar.fillAmount + DELTA_HEALTH_POINTS_PROPORTION)
        {
            state = State.Increasing;
        }
        else if(healthPointsProportion < healthPointsBar.fillAmount - DELTA_HEALTH_POINTS_PROPORTION)
        {
            state = State.Decreasing;
        }
        else
        {
            state = State.Default;
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
            //Game loop pattern 
            while (healthPointsBar.fillAmount + DELTA_HEALTH_POINTS_PROPORTION < healthPointsProportion && lagTime >= SECOND_PER_FRAME)
            {
                IncreaseHealthPoints();
                lagTime -= SECOND_PER_FRAME;                
            }   
            if(healthPointsBar.fillAmount + DELTA_HEALTH_POINTS_PROPORTION >= healthPointsProportion)
                state = State.Default;            
            break;
        case State.Decreasing:
            //Game loop pattern
            while (healthPointsBar.fillAmount - DELTA_HEALTH_POINTS_PROPORTION > healthPointsProportion && lagTime >= SECOND_PER_FRAME)
            {
                DecreaseHealthPoints();
                lagTime -= SECOND_PER_FRAME;                
            }
            if(healthPointsBar.fillAmount - DELTA_HEALTH_POINTS_PROPORTION <= healthPointsProportion)
                state = State.Default;
            break;
        }
    }

    private void ConcealWhenAnimationEnd()
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
