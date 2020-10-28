using System.Collections.Generic;
using UnityEngine;

public class ButtonSkillController : MonoBehaviour
{
    
    private List<UI_SkillButton> uI_SkillButtonList = new List<UI_SkillButton>();

    private void Awake()
    {
        foreach (Transform childTransform in transform)
        {
            uI_SkillButtonList.Add(childTransform.GetComponent<UI_SkillButton>());
        }
        uI_SkillButtonList.TrimExcess();
    }

    public void BindPlayerSkillsToUI_SkillButtonsAndShow(Player player)
    {
        for(int i = 0; i < player.GetSkillCount(); i++)
        {
            if(null == player.GetSkill(i))
            {
                uI_SkillButtonList[i].Hide();
            }
            else
            {
                uI_SkillButtonList[i].SetSkill(player.GetSkill(i));
                uI_SkillButtonList[i].UpdateSkillImageRemainingProportion();
                uI_SkillButtonList[i].Show();
            }
        }
    }

    public void HideAllUI_SkillButtons()
    {
        foreach(UI_SkillButton uI_SkillButton in uI_SkillButtonList)
        {
            uI_SkillButton.Hide();
        }
    }


}
