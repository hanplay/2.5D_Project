using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShower : MonoBehaviour
{
    private BuffSystem buffSystem;

    private List<UI_Buff> uI_BuffList = new List<UI_Buff>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            UI_Buff uI_Buff = child.GetComponent<UI_Buff>();
            if (null != uI_Buff)
                uI_BuffList.Add(uI_Buff);
        }
        HideAllUI_Buffs();
    }

    public void SetBuffSystem(BuffSystem buffSystem)
    {
        this.buffSystem = buffSystem;
        buffSystem.OnBuffsChanged += BuffSystem_OnBuffsChanged;
    }

    private void BuffSystem_OnBuffsChanged(object sender, EventArgs e)
    {
        List<Buff> buffList = buffSystem.GetBuffList();
        int size = uI_BuffList.Count;
        if (size > buffList.Count)
            size = buffList.Count;
        int i;
        for(i = 0; i < size; i++)
        {
            uI_BuffList[i].Show();
            uI_BuffList[i].SetBuff(buffList[i]);
        }

        for(; i < uI_BuffList.Count; i++)
        {
            uI_BuffList[i].Hide();
        }
        
    }

    public void HideAllUI_Buffs()
    {
        foreach(UI_Buff uI_Buff in uI_BuffList)
        {
            uI_Buff.Hide();
        }
    }

    public void UnBindBuffSystem()
    {
        buffSystem = null;
        HideAllUI_Buffs();
    }
}
