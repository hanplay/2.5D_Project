using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkillState : SkillState
{
    private float chargeTime;
    

    private float speed;
    private GameObject explosion;
    public ChargeSkillState(Unit player, Skill skill, float chargeTime, GameObject explosion) : base(player, player.GetStateSystem(), skill)
    {
        this.chargeTime = chargeTime;
        this.explosion = explosion;
    }

    public override void Begin()
    {
        base.Begin();
        speed = 4f;
        Player player = owner as Player;
        player.GetSkillSystem().SkillAction[0] = Work;
        animator.Play("Sit");
        if(0 == duration)
            duration = chargeTime + player.GetClipLength("Spell");


    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if (chargeTime < lagTime)
            animator.Play("Spell");
    }

    private void Work()
    {
        GameObject smoke = GameObject.Instantiate(explosion, owner.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        speed = 0f;
    }

    public override void Initialize()
    {
        base.Initialize();
        chargeTime = 0f;
    }

    public override bool IsTargetingState()
    {
        return false;
    }

    public override void End()
    {
        base.End();
    }

    public void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - owner.GetPosition();
        direction.y = 0f;

    }

    public void MoveTo(Unit targetUnit)
    {
        this.targetedUnit = targetUnit;
    }

}
