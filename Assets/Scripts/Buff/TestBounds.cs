using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        targetUnit.BeDamaged(12);
        Debug.Log("Damage!");
    }

    private void OnTriggerExit(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        targetUnit.Heal(12);
        Debug.Log("Heal!");
    }
}
