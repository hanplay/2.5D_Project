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
    private Unit owner;
    private BasicFXVisualizer basicFXVisualizer;

    public DieState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem)
    {
        this.owner = owner;
        basicFXVisualizer = owner.GetComponent<BasicFXVisualizer>();
    }

    public override void Begin()
    {
        base.Begin();
        duration = 3f;
        blinkTime = 1.5f;
        lagTime = 0f;
        animator.Play("Die");
        owner.GetBuffSystem().ClearBuffs();
        SetTargetable(false);        
    }


    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;

        if(lagTime > blinkTime)
        {
            basicFXVisualizer.SetSpritesVisible(isVisible);

            blinkTime += blinkCycle;
            isVisible = !isVisible;
        }
        if(lagTime > duration)
        {
            GameObject.Destroy(owner.gameObject);
        }
    }
}
