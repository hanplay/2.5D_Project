using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ChargeSkillState : SkillState, ITargetExistState
//{
//    public ChargeSkillState(Player player, SkillType skillType, bool canCancel) : base(player, skillType, canCancel)
//    {
//        this.skillType = SkillType.Charge;
//        this.canCancel = true;

//    }

//    public override void Tick(float deltaTime)
//    {
//        base.Tick(deltaTime);
//    }

//    public override void Begin()
//    {
//        animator.Play("Pierce");
//    }

//    public override bool CanBegin()
//    {
//        if (null == unit.GetTargetUnit())
//            return false;

//        if (5f > unit.ToTargetUnitDistance())
//            return true;
//        else
//            return false;
//    }

//    public override bool CanCancel()
//    {
//        return true;
//    }

//    public override void CancelBegin()
//    {
//        Begin();
//    }


//    public override void Work()
//    {
        
//    }

//    protected override void End()
//    {
        
//    }

//    public void OnTargetIsDead()
//    {
//        SetNextState(unit.GetIdleState());
//    }
//}
