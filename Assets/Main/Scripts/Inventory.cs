using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	#region Singleton
	public static Inventory instance;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
			return;
		}
		instance = this;
		player = GlobalStatic.GetPlayer().GetComponent<Player>();
	}
	#endregion

	public List<Item> listItem = new List<Item>();
	public int capacity = 10;
	private Player player;

	public bool AddItem(Item newItem)
	{
		if (listItem.Count >= capacity)
		{
			return false;
		}
		listItem.Add(newItem);  //Add item

		//Check item type
		if (newItem.myItem.GetComponent<Weapon>() != null)
		{
			Weapon newWeapon = Instantiate(newItem.myItem).GetComponent<Weapon>();
			player.AddWeapon(newWeapon);
		}

		return true;
	}

	public void RemoveItem(Item itemToRemove)
	{
		listItem.Remove(itemToRemove);
	}
}