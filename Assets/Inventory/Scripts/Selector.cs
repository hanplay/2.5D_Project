using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Selector", menuName = "Inventory System/Selector")]
public class Selector : ScriptableObject
{
	public event EventHandler OnSelect;
	public event EventHandler OnUnSelect;

	public class OnSelectEventArgs : EventArgs
	{
		public ISelectable selectableObject; 
	}

	private static Selector instance = null;

	private void OnEnable()
	{
		instance = this;
	}

	//public static Selector GetInstance()
	//{
	//	if(null == instance)
	//	{
	//		GameObject singletonObject = new GameObject(typeof(Selector).ToString());
	//		Selector selector = singletonObject.AddComponent<Selector>();
	//		instance = selector;
	//	}
	//	return instance;
	//}

	public static Selector GetInstance()
	{
		return instance;
	}

	public void OnSelectInvoke(ISelectable selectableObject)
	{
		OnSelect?.Invoke(this, new OnSelectEventArgs { selectableObject = selectableObject });
	}

	public void OnUnSelectInvoke()
	{
		OnUnSelect?.Invoke(this, EventArgs.Empty);
	}

}