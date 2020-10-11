using UnityEngine;
using GameUtility;


public class DiveSkillState : SkillState, ITargetExistsState
{
    private Unit targetUnit;
    [SerializeField]
    private GameObject smokePrefab;
    private float diveLagTime = 0f;
    private Vector3 originPosition;
    private Vector3 targetPosition;
    private float totalTime;
    private float constant = -20f;

    public DiveSkillState(Player player, SkillDatum skillDatum) : base(player, skillDatum) {}

    public override void Begin()
    {
        Debug.Log("Dive Skill Begin");
        //unit.GetComponent<Rigidbody>().useGravity = false;
        animator.Play("Jump");
        duration = 0.8f;
        totalTime = duration;
        originPosition = player.GetPosition();
        targetPosition = targetUnit.GetPosition();
        
    }


    public void OnTargetDead()
    {
        targetUnit = null;
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }


    /*
     * Tick 함수는 player.SetPosition() 함수로 deltaTime 마다 
     * player를 포물선 운동시킨다
     */
    public override void Tick(float deltaTime, Command command)
    {
        diveLagTime += deltaTime;
        player.SetPosition(new Vector3(originPosition.x + (targetPosition.x - originPosition.x) * diveLagTime / totalTime,
                                    -constant * totalTime * diveLagTime + constant * (diveLagTime * diveLagTime) + originPosition.y,
                                    originPosition.z + (targetPosition.z - originPosition.z) * diveLagTime / totalTime));
        //base.Tick(deltaTime);

    }

    //protected void End()
    //{
    //    //Object.Instantiate(smokePrefab, unit.GetPosition(), Quaternion.identity);
    //    if(null == targetUnit)
    //    {
    //        player.SetState(player.GetIdleState());
    //        player.GetState().Begin();
    //    }
    //    else
    //    {
    //        Debug.Log("Dive Damage Arise!!");

    //    }
    //}
}
