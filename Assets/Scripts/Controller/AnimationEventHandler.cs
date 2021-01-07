using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private SkillSystem skillSystem;
    private Unit owner;
    private void Start()
    {
        owner = transform.parent.GetComponent<Unit>();
        skillSystem = owner.GetSkillSystem();
    }

    private void BaseAttackAnimationEvent()
    {
        owner.GetAttackStrategy().AnimationEventOccur();
    }

    private void SkillAnimationEvent()
    {
        skillSystem.SkillAction?.Invoke();
    }

    private void DieAnimationEvent()
    {
        Destroy(gameObject);
    }

}
