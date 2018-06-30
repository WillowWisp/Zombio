using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
	[Header("Target")]
    public Transform target;
    private Vector3 initialOffset;

	[Header("Rotation")]
	public float rotationSpeed = 100;//100deg/sec
	private float targetRotation = 0;

	private void Awake()
    {
        Vector3 offset = new Vector3(0,11.72f, 0);
        transform.position = offset + target.position;

		//Initial offset
        initialOffset = target.position - transform.position;

		//Initial angle
		targetRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        transform.position = target.position - initialOffset;
    }

    private void FixedUpdate()
    {
        float rotateY = Input.GetAxisRaw("Rotate");
		
		targetRotation += rotateY * rotationSpeed * Time.fixedDeltaTime;

		Quaternion newAngle = Quaternion.Euler(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z);
		transform.rotation = Quaternion.Lerp(transform.rotation, newAngle, Time.fixedDeltaTime);
	}
}