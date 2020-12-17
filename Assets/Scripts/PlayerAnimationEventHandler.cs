using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private Unit player;
    private void Awake()
    {
        player = transform.parent.GetComponent<Unit>();
    }

    private void BaseAttackAnimationEvent()
    {
        
    }

    private void Skill0AnimationEvent()
    {
        //player.SkillAction[0]?.Invoke();
    }
    private void Skill1AnimationEvent()
    {
       // player.SkillAction[1]?.Invoke();
    }
    private void Skill2AnimationEvent()
    {
        //player.SkillAction[2]?.Invoke();
    }
    private void Skill3AnimationEvent()
    {
        //player.SkillAction[3]?.Invoke();
    }
}
