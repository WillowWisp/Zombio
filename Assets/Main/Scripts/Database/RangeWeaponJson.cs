using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class RangeWeaponJson : GeneralJson {

	public RangeWeapon targetWeapon;
	private List<RangeWeapon.BulletInfo> bulletTypeList;

	#region Default
	// Use this for initialization
	void Awake () {
		//Set the path
		shortPath = "Assets/Resources/Database/";
	}
	#endregion

	#region Method
	public override void Load()
	{
		bulletTypeList = JsonConvert.DeserializeObject<List<RangeWeapon.BulletInfo>>
			(Resources.Load<TextAsset>("Database/" + objectJsonName).ToString());

		//TODO: Xu ly prefab
		targetWeapon.bulletTypeList = bulletTypeList;
		Debug.Log("Load!");
	}
	public override void Save()
	{
		//TODO: Xu ly prefab
		bulletTypeList = targetWeapon.bulletTypeList;

		using (StreamWriter file = File.CreateText(shortPath + objectJsonName + ".json")) {
			JsonSerializer serializer = new JsonSerializer();
			serializer.Serialize(file, bulletTypeList);
		}
		Debug.Log("Saved!");
	}
	#endregion
}
