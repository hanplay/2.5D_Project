using UnityEngine.EventSystems;
using UnityEngine;

namespace GameUtility
{
	public static class Util
	{
		public static bool IsMultiTouch(PointerEventData eventData)
		{
			if (0 < eventData.pointerId)
			{
				return true;
			}
			//else
			return false;
		}
	}

	public static class LayerName
	{
		public const string Player = "Player";
		public const string Enemy = "Enemy";
		public const string Ground = "Ground";
	}


}
