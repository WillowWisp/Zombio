using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour {

    [Header("Stats")]
    public float maxHealth = 0;
    [HideInInspector] public float curHealth;
    public float damage = 0;

    [Header("Movement")]
    public float baseSpeed = 10;
	[HideInInspector] public float curSpeed;
    public float rotateSpeed = 10;          //Angle/s
	protected Animator anim;

    [Header("Effect")]
    public List<Transform> explosionList;

    protected Rigidbody rigidBody;

    //Collector
    protected Collector vfxCollector;
    
    protected virtual void Awake()
    {
		//Get Component
        rigidBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		//Get collector
		vfxCollector = GameObject.Find("VFX_Collector").GetComponent<Collector>();

		//Initial stats
		curHealth = maxHealth;
		curSpeed = baseSpeed;
    }

	//HP affects
	public void ChangeCurHealth(float value)
	{
		curHealth = Mathf.Min(curHealth + value, maxHealth);
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
		Vector3 newSpeed = direction.normalized * curSpeed;
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
		if (constraint == true) { 
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