using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameUtility;

[AddComponentMenu("Event/Player Mouse Input Module")]
public class PlayerMouseInputModule: StandaloneInputModule
{
    private Camera mainCamera;
    private int layerMask;


    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        layerMask = LayerMask.GetMask(LayerName.Player, LayerName.Enemy, LayerName.Ground);

    }

    public override void Process()
    {
        base.Process();
    }

    protected override AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
    {
        return base.GetAxisEventData(x, y, moveDeadZone);
    }

    protected override BaseEventData GetBaseEventData()
    {
        return base.GetBaseEventData();
    }

    protected override MouseState GetMousePointerEventData()
    {
        return base.GetMousePointerEventData();
    }

    protected override MouseState GetMousePointerEventData(int id)
    {
        return base.GetMousePointerEventData(id);
    }

    protected override void ProcessDrag(PointerEventData pointerEvent)
    {
        base.ProcessDrag(pointerEvent);
    }

    protected override void ProcessMove(PointerEventData pointerEvent)
    {
        base.ProcessMove(pointerEvent);
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(pointerEvent.position.x, pointerEvent.position.y));
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, 50f, layerMask);


        Unit unit = raycastHit.collider.GetComponent<Unit>();
        if(null != unit)
        {
            print("Unit!!");
        }
        else
        {
            print("Not Unit");
        }
    }

}
