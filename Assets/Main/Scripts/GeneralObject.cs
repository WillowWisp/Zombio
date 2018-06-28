using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour {

    [Header("Stats")]
    public float maxHealth = 0;
    private float curHealth;
    public float damage = 0;

    [Header("Movement")]
    public float baseSpeed = 10;
	protected float curSpeed;
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
    protected virtual void Update()
    {
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
	public virtual void Move(Vector3 direction, Space relativeTo = Space.Self)
    {
        transform.Translate(direction * curSpeed * Time.deltaTime, relativeTo);
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
			valueToChange = newValue;
		}
	}
}