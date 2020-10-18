using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaOfEffectModule : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private int maxCount;
    private Unit[] targetUnits;

    private void Awake()
    {
        targetUnits = new Unit[maxCount];
    }

    private void Update()
    {
        if(0 > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(null != enemy)
        {

        }
    }

    

}
