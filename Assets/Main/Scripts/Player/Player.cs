using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralObject {
	bool isAiming = false;
	bool isMoving = false;

	private void FixedUpdate()
	{
		//Reset speed
		curSpeed = baseSpeed;

		//Get input
		float moveSide = Input.GetAxis("Horizontal");
		float moveForward = Input.GetAxis("Vertical");

		if (moveSide != 0 || moveForward != 0)
		{
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}

		Vector3 moveDirection = GetMoveDirection(moveSide, moveForward);


		//Rotate
		RotateFaceDirection(moveDirection);

		//Move
		Move(moveDirection, Space.World);
	}

	//Misc
	Vector3 GetMoveDirection(float moveSide, float moveForward)
	{
		//Input must be relative to camera rotation
		Transform cameraParent = Camera.main.transform.parent;
		Vector3 cameraForward = cameraParent.forward;
		Vector3 cameraRight = cameraParent.right;

		Vector3 moveDirection = new Vector3();

		Vector3 moveX = cameraRight * moveSide;
		Vector3 moveZ = cameraForward * moveForward;
		return moveDirection = moveX + moveZ;
	}

	void RotateFaceDirection(Vector3 moveDirection)
	{
		if (Input.GetMouseButton(1) == false)
		{
			if (isMoving)
			{
				RotateToDirection(moveDirection);
			}
		}
		else
		{
			Vector3 mouseLocation = Input.mousePosition;
			Vector3 playerToMousePos2D = mouseLocation - Camera.main.WorldToScreenPoint(transform.position);

			Vector3 faceDirection = new Vector3(playerToMousePos2D.x, 0, playerToMousePos2D.y);

			RotateToDirection(faceDirection);

			float differenceAngle = Vector3.Angle(moveDirection, transform.forward);
			
			//Reduce speed if moving backward
			ChangeStat(ref curSpeed, baseSpeed / 4.0f, differenceAngle >= 120);
		}
	}
}	