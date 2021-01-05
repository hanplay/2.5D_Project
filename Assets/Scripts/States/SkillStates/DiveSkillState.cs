using UnityEngine;

public class DiveSkillState : SkillState
{
    private GameObject hitSmoke;
    private GameObject smokeExplosion;
    private IDamageStrategy damageStrategy = new CommonDamageStrategy();
    private int damage;
    private float radius;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private float totalTime;
    private float constant = -20f;
    public DiveSkillState(Unit player, Skill skill, int damage, float radius, GameObject hitSmoke, GameObject smokeExplosion) : base(player, player.GetStateSystem(), skill) 
    {
        this.damage = damage;
        this.radius = radius;
        this.hitSmoke = hitSmoke;
        this.smokeExplosion = smokeExplosion;
        duration = 0.8f;
    }
    public override void Begin()
    {
        base.Begin();

        animator.Play("Jump");
        totalTime = duration;
        originPosition = owner.GetPosition();

        targetPosition = owner.GetTargetedUnitHandler().GetTargetedUnit().GetPosition();
        Vector3 direction = (targetPosition - originPosition).normalized;
        targetPosition = targetPosition - 1 * direction;
        GameObject.Instantiate(hitSmoke, owner.GetPosition(), Quaternion.Euler(90f, 0f,0f));
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        /*
         * Tick 함수는 player.SetPosition() 함수로 deltaTime 마다 
         * player를 포물선 운동시킨다
         */
        owner.SetPosition(new Vector3(originPosition.x + (targetPosition.x - originPosition.x) * lagTime / totalTime,
                                    -constant * totalTime * lagTime + constant * (lagTime * lagTime) + originPosition.y,
                                    originPosition.z + (targetPosition.z - originPosition.z) * lagTime / totalTime));

    }

    public override void End()
    {
        base.End();
        GameObject smoke = GameObject.Instantiate(smokeExplosion, owner.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        Collider[] colliders = Physics.OverlapSphere(owner.GetPosition(), 2f);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent<Unit>(out Unit targetedUnit))
            {
                if (false == owner.IsTargetable(targetedUnit))
                    continue;
                targetedUnit.GetBuffSystem().AddBuff(GameAssets.Instance.CreateBuff(BuffType.Stun));
                damageStrategy.Do(targetedUnit, damage);
            }
        }
    }

}
