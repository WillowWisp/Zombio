using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public Transform target;
    private bool isRotating = false;
    private Vector3 initialOffset;

    private void Awake()
    {
        Vector3 offset = new Vector3(0,11.72f, 0);
        transform.position = offset + target.position;

        initialOffset = target.position - transform.position;       
    }

    private void Update()
    {
        transform.position = target.position - initialOffset;
    }

    private void FixedUpdate()
    {
        float rotateY = Input.GetAxisRaw("Rotate");
        if (rotateY == 0) {
            isRotating = false;
        } else {
            if (isRotating == false) {
                Vector3 newDirection = transform.eulerAngles;
                newDirection.y += 90 * rotateY;
                transform.eulerAngles = newDirection;
                isRotating = true;
            }
        }
    }
}
