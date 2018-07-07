using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Sprite icon;

	public virtual void UseWeapon() { }

	public virtual void Reload() { }

	public virtual void StopWeapon() { }
}
