using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralJson : MonoBehaviour {

	public string objectJsonName = "TestWeapon";
	protected string shortPath;

	#region Action
	public void SetPath(string jsonName)
	{
		objectJsonName = jsonName;
	}
	public virtual void Load()
	{
	}
	public virtual void Save()
	{
	}
	#endregion
}
