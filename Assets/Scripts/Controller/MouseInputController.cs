using System;
using UnityEngine;
using GameUtility;

public class MouseInputController : MonoBehaviour
{
    public event EventHandler OnPlayerClicked;
    public class OnPlayerClickedEvent : EventArgs
    {
        public Player clickedPlayer;
    }

    private Player selectedPlayer;
    private Vector3 downedPosition;
    private Unit previousTargetedUnit;
    private bool isDragging;

    private Camera mainCamera;
    int layerMask;

    private void Awake()
    {
        mainCamera = Camera.main;
        layerMask = LayerMask.GetMask(LayerName.Player, LayerName.Enemy, LayerName.Ground);
    }

    
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, 50f, layerMask);

        if(Input.GetMouseButtonDown(0))
        {
            Player player = raycastHit.collider.GetComponent<Player>();
            OnCursorDownPlayer(player);
            downedPosition = raycastHit.point;
        }

        else if(Input.GetMouseButton(0))
        {
            if (null == selectedPlayer)
                return;

            if (downedPosition == raycastHit.point)
            {
                isDragging = false;
                return;
            }
            else
            {
                isDragging = true;
            }


            Unit targetUnit = raycastHit.collider.GetComponent<Unit>();
            if(selectedPlayer.IsTargetable(targetUnit))
            {
                if (previousTargetedUnit != targetUnit)
                {
                    OnCursorDragEnterUnit(targetUnit);
                    if(null != previousTargetedUnit)
                        OnCursorDragExitUnit(previousTargetedUnit);
                    previousTargetedUnit = targetUnit;
                }
                selectedPlayer.GetTravelRouteWriter().DrawRouteLine(targetUnit);
            }
            else
            {
                if(null != previousTargetedUnit)
                {
                    OnCursorDragExitUnit(previousTargetedUnit);
                    previousTargetedUnit = null;
                }
                selectedPlayer.GetTravelRouteWriter().DrawRouteLine(raycastHit.point);
            }
        }
        
        else if(Input.GetMouseButtonUp(0))
        {
            if (null == selectedPlayer)
                return;            
            Unit targetUnit = raycastHit.collider.GetComponent<Unit>();

            if(selectedPlayer == targetUnit as Player)
            {
                OnCursorClick();
                return;
            }

            if(selectedPlayer.IsTargetable(targetUnit))
            {
                selectedPlayer.GetState().SetCommand(new AttackCommand(selectedPlayer, targetUnit));                
            }
            else
            {
                selectedPlayer.GetState().SetCommand(new MoveCommand(selectedPlayer, raycastHit.point));
            }

            selectedPlayer.GetTravelRouteWriter().HideRouteLine();
            selectedPlayer = null;
        }

    
    }


    private void OnCursorDownPlayer(Player player)
    {
        //if(null != selectedPlayer)
        //{
        //    selectedPlayer.OnDead -= SelectedPlayer_OnDead;
        //}
        selectedPlayer = player;
        //selectedPlayer.OnDead += SelectedPlayer_OnDead;
    }

    //private void SelectedPlayer_OnDead(object sender, EventArgs e)
    //{
    //    selectedPlayer.GetTravelRouteWriter().HideRouteLine();
    //    selectedPlayer.OnDead -= SelectedPlayer_OnDead;
    //    selectedPlayer = null;
    //}

    private void OnCursorDragEnterUnit(Unit unit)
    {
        unit.GetBasicFXVisualizer().GlowFadeIn(new Color(0f, 0.2f, 0f));
    }

    private void OnCursorDragExitUnit(Unit unit)
    {
        unit.GetBasicFXVisualizer().GlowFadeOut(new Color(0f, 0.2f, 0f));
    }


    private void OnCursorUp()
    {
    }

    //The click is taking care of down and up signals in same position
    private void OnCursorClick()
    {
        OnPlayerClicked.Invoke(this, new OnPlayerClickedEvent { clickedPlayer = selectedPlayer });
        selectedPlayer.GetTravelRouteWriter().HideRouteLine();
    }


}
