using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputListener : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;
    private BasicFXVixualizer basicFXVixualizer;
    private TravelRouteWriter travelRouteWriter;

    private void Awake()
    {
        player = GetComponent<Player>();
        basicFXVixualizer = GetComponent<BasicFXVixualizer>();
        travelRouteWriter = GetComponent<TravelRouteWriter>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //단지 OnPointerUp의 동작을 위해 Implementing
    }

    public void OnDrag(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        Unit unit = raycastResult.gameObject.GetComponent<Unit>();
        if(player.IsTargetable(unit))
        {
            travelRouteWriter.DrawRouteLine(unit.GetPosition());
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
        Unit unit = raycastResult.gameObject.GetComponent<Unit>();
        if (player.IsTargetable(unit))
        {
            player.SetCommand(new AttackCommand(player, unit));
        }
        else
        {
            player.SetCommand(new MoveCommand(player, raycastResult.worldPosition));
        }


        if(player == unit as Player)
        {
            ButtonSkillController.GetInstance().SetPlayer(player);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        basicFXVixualizer.GlowFadeIn(new Color(0f, 0.1f, 0f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        basicFXVixualizer.GlowFadeOut(new Color(0f, 0.1f, 0f));
    }

}
