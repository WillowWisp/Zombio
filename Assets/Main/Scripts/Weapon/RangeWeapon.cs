using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon {

	[System.Serializable]   //Cái này để hiện ra inspector
	public class BulletInfo
	{
		[Header("Bullet info")]
		public float damage = 10;
		public float speed = 10;        //Nên dùng speed hay dùng force?
		public bool useGravity = false;
		public float range = 10;
		public GameObject bulletPrefab;

		[Header("Ammo Type")]
		public int totalAmmoLeft = 0;
		public int magazineSize = 0;
		public int curAmmoLeft = 0;		//Ammo left in the magazine
		//public int maxAmmo = 0;
		public float fireRate = 1;		//Bullet/s	=> f = 1/fireRate =]]		
	}

	[Header("Bullet Type")]
	public List<BulletInfo> bulletTypeList;
	private BulletInfo curBulletType;

	[Header("Weapon info")]
	private Coroutine shooting;
	public Transform firePoint;
	private Collector bulletCollector;

	#region Default
	private void Awake()
	{
		curBulletType = bulletTypeList[0];
		bulletCollector = GameObject.Find("Object_Collector").GetComponent<Collector>();
	}
	#endregion

	#region Action
	public virtual void ShootBullet()
	{
		Debug.Log("Tới đây rồi nè!");
		
		//TODO: làm gì đó...
		curBulletType.curAmmoLeft--;
		Vector3 shotVector = firePoint.position - transform.position;

		Projectile shotBullet = Instantiate(curBulletType.bulletPrefab, firePoint.position, Quaternion.identity).GetComponent<Projectile>();

		//Set default stat
		shotBullet.baseSpeed = curBulletType.speed;
		shotBullet.curSpeed = curBulletType.speed;
		shotBullet.damage = curBulletType.damage;
		shotBullet.range = curBulletType.range;
		shotBullet.GetComponent<Rigidbody>().useGravity = curBulletType.useGravity;
		shotBullet.ChangeTag(tag);

		//Adopting
		bulletCollector.AddChild(shotBullet.transform);
		Debug.Log("ShotVector: " + shotVector);
		//Launch the bullet
		shotBullet.Move(shotVector);
	}
	public IEnumerator StartShooting()
	{
		ShootBullet();
		yield return new WaitForSeconds(1 / curBulletType.fireRate);
		shooting = null;
	}
	public override void UseWeapon()
	{
		if (shooting != null)
		{
			return;
		}

		if (curBulletType.curAmmoLeft <= 0)
		{
			//TODO: Play some animation to indicate we are running out of ammo

			return;
		}
		shooting = StartCoroutine(StartShooting());
	}
	public void NextBullet()
	{
		int curBulletIndx = bulletTypeList.IndexOf(curBulletType);
		curBulletIndx = curBulletIndx >= bulletTypeList.Count ? 0 : curBulletIndx + 1;
		curBulletType = bulletTypeList[curBulletIndx];
		//TODO : Change sprite

	}
	public override void Reload()
	{
		if (curBulletType.totalAmmoLeft <= 0)
		{
			//TODO : Play some animation
			return;
		}
		int ammoToReload = curBulletType.magazineSize - curBulletType.curAmmoLeft;
		ammoToReload = Mathf.Min(ammoToReload, curBulletType.totalAmmoLeft);
		curBulletType.curAmmoLeft += ammoToReload;
		curBulletType.totalAmmoLeft -= ammoToReload;
	}
	#endregion
}
