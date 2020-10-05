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
		public const string UnPointable = "UnPointable";
	}

	public struct StateType
    {
		public const int Basic = 0x0;				//000000
		public const int Skill = 0x1;				//000001
		public const int TargetExist = 0x2;			//000010
		public const int CanCancel = 0x4;			//000100
		public const int CannotBeCanceled = 0x8;	//001000
    }
}
