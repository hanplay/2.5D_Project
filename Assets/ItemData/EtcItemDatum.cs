using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Etc Item Datum", menuName = "Inventory System/ItemDatum/Etc Item Datum")]
public class EtcItemDatum : ItemDatum, IStackableItemDatum, ISerializationCallbackReceiver
{
    [SerializeField]
    private int maxAmount;

    public void OnAfterDeserialize()
    {
        itemType = ItemType.Etc;
    }

    public void OnBeforeSerialize() { }

    public int GetMaxAmount()
    {
        return maxAmount;
    }

}
