using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private SkillSystem skillSystem;
    private AttackSystem attackSystem;
    private void Awake()
    {
        Unit owner = transform.parent.GetComponent<Unit>();
        skillSystem = owner.GetSkillSystem();
        attackSystem = owner.GetAttackSystem();
    }

    private void BaseAttackAnimationEvent()
    {
        attackSystem.GetAttackStrategy().AnimationEventOccur();
    }

    private void SkillAnimationEvent()
    {
        skillSystem.SkillAction?.Invoke();
    }

}
