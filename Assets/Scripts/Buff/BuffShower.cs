using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffShower : MonoBehaviour
{
    private Player player;
    private List<UI_Buff> uI_Buffs = new List<UI_Buff>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            UI_Buff uI_Buff = child.GetComponent<UI_Buff>();
            if (null != uI_Buff)
                uI_Buffs.Add(uI_Buff);
        }
    }



    public void SetPlayer(Player player)
    {
        this.player = player;
    }


}
