using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    private float duration;
    private float blinkCycle = 0.1f;
    private float blinkTime;
    private float lagTime;
    private bool isVisible = false;
    private BasicFXVisualizer basicFXVisualizer;

    public DieState(Unit player, StateSystem stateSystem, float duration) : base(player, stateSystem, Die)
    {
        this.duration = duration;
        basicFXVisualizer = player.GetComponent<BasicFXVisualizer>();
    }

    public override void Begin()
    {
        blinkTime = 2.5f;
        lagTime = 0f;
        animator.Play("Die");
    }


    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;
        if(lagTime > blinkTime)
        {
            basicFXVisualizer.SetSpritesVisible(isVisible);

            blinkTime += blinkCycle;
            isVisible = Toggle(isVisible);
        }
        if(lagTime > duration)
        {
            GameObject.Destroy(owner.gameObject);
        }
    }

    private bool Toggle(bool value)
    {
        if (false == value)
            return true;
        else
            return false;
    }
    public override bool IsTargetingState()
    {
        return false;
    }
}
