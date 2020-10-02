using UnityEngine;

public class AuraBuffModule : MonoBehaviour
{

	private float buffRange = 2.5f;
	private CapsuleCollider collider;
	private AuraBuff auraBuff;

	public AuraBuffModule(AuraBuff auraBuff)
	{
		this.auraBuff = auraBuff;
	}
	private void Awake()
	{
		collider = new CapsuleCollider();
		collider.direction = 1;  // Y-Axis //  0: X-Axis, 2: Z-Axis
		collider.height = buffRange * 2f + 3f;
		collider.radius = buffRange;
		collider.isTrigger = true;
	}
	

	private void OnTriggerEnter(Collider other)
	{
		Player player = other.GetComponent<Player>();
		if(null != player)
		{
										
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Player player = other.GetComponent<Player>();
		if(null != player)
		{
			
		}
	}
}
