using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputListener : MonoBehaviour, 
    IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;
    private BasicFXVisualizer basicFXVisualizer;
    private TravelRouteWriter travelRouteWriter;

    private MoveCommand moveCommand;
    private ChaseCommand chaseCommand;

    private void Awake()
    {
        player = GetComponent<Player>();
        basicFXVisualizer = GetComponent<BasicFXVisualizer>();
        travelRouteWriter = GetComponent<TravelRouteWriter>();

        moveCommand = new MoveCommand();
        chaseCommand = new ChaseCommand();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //단지 OnPointerUp의 동작을 위해 Implementing
    }

    public void OnDrag(PointerEventData eventData)
    {
        IMoveableState moveableState = player.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;

        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        Unit unit = raycastResult.gameObject.GetComponent<Unit>();
        if(player.IsTargetable(unit))
        {
            travelRouteWriter.DrawRouteLine(unit);
        }   
        else
        {
            travelRouteWriter.DrawRouteLine(raycastResult.worldPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        travelRouteWriter.HideRouteLine();
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        Unit unit;
        if(false == raycastResult.gameObject.TryGetComponent<Unit>(out unit))
        {
            moveCommand.Execute(player, raycastResult.worldPosition);
            return;
        }      
        
        if(player == unit)
        {
            PlayerSelector.GetInstance().SetPlayer(player);
            return;
        }
        if (player.IsTargetable(unit))
        {
            chaseCommand.Execute(player, unit);
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        basicFXVisualizer.GlowFadeIn(new Color(0f, 0.1f, 0f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        basicFXVisualizer.GlowFadeOut(new Color(0f, 0.1f, 0f));
    }
}
