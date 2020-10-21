using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelRouteWriter : MonoBehaviour
{
    private Unit targetUnit;
    private Transform travelRouteLine;
  
    private void Awake()
    {
        travelRouteLine = transform.Find("TravelRouteLine");
    }

    private void DrawRouteLine(Vector3 characterPosition, Vector3 groundPosition)
    {
        travelRouteLine.gameObject.SetActive(true);
        Vector3 toPointerPosition = groundPosition - characterPosition;
        toPointerPosition.y = 0f;
        float length = toPointerPosition.magnitude;
        float angle = Mathf.Atan2(toPointerPosition.x, toPointerPosition.z) * Mathf.Rad2Deg;
        travelRouteLine.rotation = Quaternion.Euler(0f, angle, 0f);
        travelRouteLine.localScale = new Vector3(1f, 1f, length);
    }

    private void Update()
    {
        if (null == targetUnit)
            return;
        DrawRouteLine(transform.position, targetUnit.GetPosition());

    }

    public void DrawRouteLine(Vector3 groundPosition)
    {
        targetUnit = null;
        DrawRouteLine(transform.position, groundPosition);
    }

    public void DrawRouteLine(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public void HideRouteLine()
    {
        targetUnit = null;
        travelRouteLine.gameObject.SetActive(false);
        
    }

}
