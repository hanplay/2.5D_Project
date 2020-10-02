using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Potion Item Datum", menuName = "Inventory System/ItemDatum/Potion Item Datum")]
public class PotionItemDatum : ItemDatum, IStackableItemDatum, ISerializationCallbackReceiver
{
    [SerializeField]
    private int maxAmount;

    public void OnAfterDeserialize()
    {
        itemType = ItemType.Potion;
    }
    public void OnBeforeSerialize() { }

    public int GetMaxAmount()
    {
        return maxAmount;
    }
}
