using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour {

    [Header("Stats")]
    public float maxHealth = 0;
    protected float curHealth;
    public float damage = 0;

    [Header("Movement")]
    public float baseSpeed = 10;
	[HideInInspector] public float curSpeed;
    public float rotateSpeed = 10;          //Angle/s

    [Header("Effect")]
    public List<Transform> explosionList;

    protected Rigidbody rigidBody;

    //Collector
    protected Collector vfxCollector;
    
    protected virtual void Awake()
    {
		//Get Component
        rigidBody = GetComponent<Rigidbody>();
		//Get collector
		vfxCollector = GameObject.Find("VFX_Collector").GetComponent<Collector>();

		//Intial stats
		curHealth = maxHealth;
		curSpeed = baseSpeed;
    }

	//HP affects
	public void ChangeCurHealth(float value)
	{
		curHealth += value;
		CheckHealth();
	}
	public virtual void CheckHealth()
	{
		if (maxHealth <= 0)
		{
			return;
		}
		if (curHealth <= 0)
		{
			Die();
		}
	}

	//Action
	public virtual void Move(Vector3 direction)
    {
		Vector3 newSpeed = direction * curSpeed;
		newSpeed.y = rigidBody.velocity.y;
		rigidBody.velocity = newSpeed;

    }
	public virtual void RotateToDirection(Vector3 moveDirection)
	{
		Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * rotateSpeed);
	}
    public virtual void Die()
    {
        //Create explosion
        for (int indx = 0; indx < explosionList.Count; indx++) {
            Transform boom = Instantiate(explosionList[indx], transform.position, Quaternion.identity);
            vfxCollector.AddChild(boom);
        }

        Destroy(gameObject);
    }

	//Misc
	public virtual void ChangeStat(ref float valueToChange, float newValue, bool constraint) {
		if (constraint == true)
		{
			Debug.Log(newValue + " " + valueToChange);
			valueToChange = newValue;
		}
	}
	public void ChangeTag(string newTag)
	{
		tag = newTag;
		foreach (Transform child in transform)
		{
			tag = newTag;
		}
	}
}