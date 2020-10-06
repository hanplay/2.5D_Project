using UnityEngine;
using GameUtility;

public class DiveSkillState : SkillState
{
    [SerializeField]
    private GameObject smokePrefab;
    private float diveLagTime = 0f;
    private Vector3 originPosition;
    private Vector3 targetPosition;
    private float totalTime;
    private float constant = -20f;

    public DiveSkillState(Player player) : 
        base(player, StateType.Skill | StateType.CanCancel | StateType.TargetExist | StateType.CannotBeCanceled)
    { }

    public override void Begin()
    {
        Debug.Log("Dive Skill Begin");
        //unit.GetComponent<Rigidbody>().useGravity = false;
        animator.Play("Jump");
        duration = 0.8f;
        totalTime = duration;
        originPosition = unit.GetPosition();
        targetPosition = unit.GetTargetUnit().GetPosition();
        
    }

    public override bool CanBegin()
    {
        return true;
    }


    /*
     * Tick 함수는 player.SetPosition() 함수로 deltaTime 마다 player를 
     */
    public override void Tick(float deltaTime)
    {
        diveLagTime += deltaTime;
        unit.SetPosition(new Vector3(originPosition.x + (targetPosition.x - originPosition.x) * diveLagTime / totalTime,
                                    -constant * totalTime * diveLagTime + constant * (diveLagTime * diveLagTime) + originPosition.y,
                                    originPosition.z + (targetPosition.z - originPosition.z) * diveLagTime / totalTime));
        base.Tick(deltaTime);

    }

    protected override void End()
    {
        Debug.Log("Dive Damage Arise!!");
        //Object.Instantiate(smokePrefab, unit.GetPosition(), Quaternion.identity);
        base.End();
    }
}
