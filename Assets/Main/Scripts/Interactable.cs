using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	public virtual void Interact()
	{
		Debug.Log("Override me");
	}

	public virtual void Option_1()
	{

	}
}