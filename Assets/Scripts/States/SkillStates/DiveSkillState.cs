using UnityEngine;

public class DiveSkillState : SkillState, ITargetExistsState
{
    private GameObject smokePrefab;
    private float diveLagTime = 0f;
    private Vector3 originPosition;
    private Vector3 targetPosition;
    private float totalTime;
    private float constant = -20f;

    public DiveSkillState(Player player, Skill skill, GameObject smokePrefab) : base(player, skill) 
    {
        this.smokePrefab = smokePrefab;
    }
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

    /*
     * Tick 함수는 player.SetPosition() 함수로 deltaTime 마다 
     * player를 포물선 운동시킨다
     */
    public override void TickAccept(float deltaTime, Command command)
    {
        if(IsEnd())
        {
            End();
        }
        diveLagTime += deltaTime;
        player.SetPosition(new Vector3(originPosition.x + (targetPosition.x - originPosition.x) * diveLagTime / totalTime,
                                    -constant * totalTime * diveLagTime + constant * (diveLagTime * diveLagTime) + originPosition.y,
                                    originPosition.z + (targetPosition.z - originPosition.z) * diveLagTime / totalTime));

    }



    protected void End()
    {
        GameObject smoke = GameObject.Instantiate(smokePrefab, player.GetPosition(), Quaternion.identity);
        
    }
}
