using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkillState : SkillState
{
    private float chargeTime;
    

    private float speed;
    private GameObject explosion;
    public ChargeSkillState(Player player, Skill skill, float chargeTime, GameObject Explosion) : base(player, skill)
    {
        this.chargeTime = chargeTime;
    }

    public override void Begin()
    {
        base.Begin();
        speed = 4f;
        player.SkillAction[0] = Work;
        animator.Play("Sit");
        if(0 == duration)
            duration = chargeTime + player.GetClipLength("Spell");


    }
    public override void TickAccept(float deltaTime, Command command)
    {
        base.TickAccept(deltaTime, command);
        if (chargeTime < lagTime)
            animator.Play("Spell");
    }

    private void Work()
    {
        GameObject smoke = GameObject.Instantiate(explosion, player.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        speed = 0f;
    }

    public override void Initialize()
    {
        base.Initialize();
        chargeTime = 0f;
    }

    public override bool IsTargetIngState()
    {
        return false;
    }

    protected override void End()
    {
        base.End();
    }

    public void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - player.GetPosition();
        direction.y = 0f;

    }

    public void MoveTo(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

}
