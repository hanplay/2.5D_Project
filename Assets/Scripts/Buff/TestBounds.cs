﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounds : MonoBehaviour
{
    IDamageStrategy damageStrategy = new TrueDamageStrategy();
    IDamageStrategy healStrategy = new HealStrategy();
    private void OnTriggerEnter(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        damageStrategy.Do(targetUnit, 10);
        Debug.Log("Damage!");
    }

    private void OnTriggerExit(Collider collider)
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        healStrategy.Do(targetUnit, 10);
        Debug.Log("Heal!");
    }
}
