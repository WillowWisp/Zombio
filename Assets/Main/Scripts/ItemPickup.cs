using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;

	public override void Interact()
	{
		base.Interact();
		if (Inventory.instance.AddItem(item) == true)
		{
			Debug.Log("Picking up " + item.name);
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("Deo pick duoc");
		}
	}

}