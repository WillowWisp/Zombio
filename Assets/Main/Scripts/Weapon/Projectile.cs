using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GeneralObject {

	[Header("Bullet info")]
	public float range = -1;
	public Sprite myIcon;
	private Vector3 initialPos;

	#region Default
	protected override void Awake()
	{
		base.Awake();
		initialPos = transform.position;
	}
	protected void FixedUpdate()
	{
		if ((transform.position - initialPos).magnitude >= range)
		{
			Die();
		}
	}
	#endregion

	private void OnCollisionEnter(Collision collision)
	{
		GeneralObject otherScript = collision.transform.GetComponent<GeneralObject>();
		if (otherScript != null)
		{
			if ((otherScript.tag == "Enemy" &&  tag == "Ally") ||
				(tag == "Enemy" && otherScript.tag == "Ally")){
				otherScript.ChangeCurHealth(-damage);

				curHealth -= 1;
				if (curHealth <= 0)
				{
					//Tự nổ
					Die();
				}
			}
		}
		else
		{
			Debug.Log("collide");
			Die();	//Va chạm với tường chẳng hạn
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		GeneralObject otherScript = collision.transform.GetComponent<GeneralObject>();
		if (otherScript != null)
		{
			if ((otherScript.tag == "Enemy" && tag == "Ally") ||
				(tag == "Enemy" && otherScript.tag == "Ally"))
			{
				otherScript.ChangeCurHealth(-damage);

				curHealth -= 1;
				if (curHealth <= 0)
				{
					//Tự nổ
					Die();
				}
			}
		}
		else
		{
			Die();  //Va chạm với tường chẳng hạn
		}
	}
}
