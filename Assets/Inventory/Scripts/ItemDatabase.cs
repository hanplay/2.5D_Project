using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Database", menuName = "Inventory System/Item/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
	[SerializeField]
	private ItemDatum[] itemData;
	[SerializeField]
	private Sprite neonSquareSprite;


	public static ItemDatabase instance{ private set; get; }

	public void OnAfterDeserialize()
	{
		instance = this;
		for(int i = 0; i < itemData.Length; i++)
		{
			itemData[i].SetId(i);
		}
		
	}

	public void OnBeforeSerialize()
	{
		
	}

	public Sprite GetNeonSquareSprite()
	{
		return neonSquareSprite;
	}
}
