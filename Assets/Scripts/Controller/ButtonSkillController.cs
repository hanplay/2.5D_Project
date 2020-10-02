using UnityEngine;
using GameUtility;

public class ButtonSkillController : MonoBehaviour
{
    private Camera mainCamera;
    private Player player;
    private int layerMask;

    private UI_SkillButton[] uI_SkillButtonList = new UI_SkillButton[Player.SkillCount];

    //Singletone
    private static ButtonSkillController instance;
    public static ButtonSkillController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        layerMask = LayerMask.GetMask(LayerName.Player, LayerName.Enemy, LayerName.Ground);
    }

    private void Update()
    {


    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

}
