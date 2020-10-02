using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentSystem 
{
    public event EventHandler OnEquip;
	public event EventHandler OnUnequip;

    private Action<EquipmentItem> OnEquipmentItemAction;
    
    public class OnEquipmentChangedArgs : EventArgs
    {   
        public int attackPower;
        public int percentAttackPower;
        public int armor;
    }    
    public void Equip()
    {
        OnEquipmentItemAction += ddd;
        
	}

    public void ddd(EquipmentItem equipmentItem)
    {
        
	}
    public void Unequip()
    {
        
	}    
}
