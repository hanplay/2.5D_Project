using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BasicState
{
    private float duration;
    private float blinkCycle = 0.1f;
    private float blinkTime;
    private float lagTime;
    private bool isVisible = false;
    private BasicFXVisualizer basicFXVisualizer;

    public DieState(Player player, float duration) : base(player)
    {
        this.duration = duration;
        basicFXVisualizer = player.GetComponent<BasicFXVisualizer>();
    }

    public override void Begin()
    {
        blinkTime = 2.5f;
        lagTime = 0f;
        animator.Play("Die");
        player.OnDie();
    }

    public override void TickAccept(float deltaTime, Command command)
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
            GameObject.Destroy(player.gameObject);
        }
    }

    private bool Toggle(bool value)
    {
        if (false == value)
            return true;
        else
            return false;
    }

}
