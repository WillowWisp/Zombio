using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour {

    public Transform target;
    private Vector3 initialOffset;

    private void Awake()
    {
        initialOffset = target.position - transform.position;    
    }

    private void Update()
    {
        transform.position = target.position - initialOffset;
    }
}
