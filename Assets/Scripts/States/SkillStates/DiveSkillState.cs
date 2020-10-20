﻿using UnityEngine;

public class DiveSkillState : SkillState, ITargetExistsState
{
    private GameObject hitSmoke;
    private GameObject smokeExplosion;
    private float lagTime = 0f;
    private Vector3 originPosition;
    private Vector3 targetPosition;
    private float totalTime;
    private float constant = -20f;
    public DiveSkillState(Player player, Skill skill, GameObject hitSmoke, GameObject smokeExplosion) : base(player, skill) 
    {
        this.hitSmoke = hitSmoke;
        this.smokeExplosion = smokeExplosion;
        duration = 0.8f;
    }
    public override void Begin()
    {
        base.Begin();
        Debug.Log("Dive Skill Begin");
        //unit.GetComponent<Rigidbody>().useGravity = false;
        animator.Play("Jump");
        totalTime = duration;
        originPosition = player.GetPosition();
        targetPosition = targetUnit.GetPosition();
        GameObject.Instantiate(hitSmoke, player.GetPosition(), Quaternion.identity);
    }


    public void OnTargetDead()
    {
        targetUnit = null;
    }

    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);
        if (true == isEnd)
            return;
        lagTime += deltaTime;
        if(lagTime >= duration)
        {
            End();
        }
        /*
         * Tick 함수는 player.SetPosition() 함수로 deltaTime 마다 
         * player를 포물선 운동시킨다
         */
        player.SetPosition(new Vector3(originPosition.x + (targetPosition.x - originPosition.x) * lagTime / totalTime,
                                    -constant * totalTime * lagTime + constant * (lagTime * lagTime) + originPosition.y,
                                    originPosition.z + (targetPosition.z - originPosition.z) * lagTime / totalTime));

    }

    protected void End()
    {
        isEnd = true;
        GameObject smoke = GameObject.Instantiate(smokeExplosion, player.GetPosition(), Quaternion.identity);
    }
    public override void Initialize()
    {
        isEnd = false;
        lagTime = 0f;
    }    
}
