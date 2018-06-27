using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour {

    [Header("Stats")]
    public float maxHealth = 0;
    private float curHealth;
    public float damage = 0;

    [Header("Movement")]
    public float speed = 10;

    [Header("Effect")]
    public List<Transform> explosionList;

    protected Rigidbody rigidBody;

    //Collector
    protected Collector vfxCollector;
    
    protected virtual void Awake()
    {
        curHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody>();

        //Get collector
        vfxCollector = GameObject.Find("VFX_Collector").GetComponent<Collector>();
    }
    protected virtual void Update()
    {
    }

    //Action
    public void ChangeCurHealth(float value)
    {
        curHealth += value;
        CheckHealth();
    }
    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
    public virtual void CheckHealth()
    {
        if (maxHealth <= 0) {
            return;
        }
        if (curHealth <= 0) {
            Die();
        }
    }
    public virtual void Attack() { }
}
