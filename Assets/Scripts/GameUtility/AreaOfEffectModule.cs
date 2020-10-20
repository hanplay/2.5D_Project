using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AreaOfEffectModule : MonoBehaviour
{
    private Player owner;
    [SerializeField]
    private float lifeTime;

    private int damage;
    private Buff buff;


    public AreaOfEffectModule(Player owner, float lifeTime)
    {
        this.owner = owner;
        this.lifeTime = lifeTime;
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if(0 > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (false == owner.IsTargetable(unit))
            return;

        if(null != buff)
        {
            unit.AddBuff(buff);
        }
    }
}
