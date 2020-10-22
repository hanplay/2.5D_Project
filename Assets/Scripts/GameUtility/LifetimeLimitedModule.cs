using UnityEngine;

public class LifetimeLimitedModule : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 3f;
 
    void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if(0 > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
