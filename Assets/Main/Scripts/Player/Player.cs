using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralObject {

	[Header("Status")]
	public float maxAngleDifference = 120;
	private bool isMoving = false;

	[Header("Weapon thingy")]
	public List<Weapon> weaponList;
	private Weapon curWeapon;

	protected override void Awake()
	{
		base.Awake();
		curWeapon = weaponList[0];
	}
	private void FixedUpdate()
	{
		//Reset speed
		curSpeed = baseSpeed;

		//Check if running
		bool isRunning = Input.GetButton("Run");
		Debug.Log("TODO: Only run when there is stamina");
		ChangeStat(ref curSpeed, baseSpeed * 2, isRunning);	
		
		//Get input
		float moveSide = Input.GetAxisRaw("Horizontal");
		float moveForward = Input.GetAxisRaw("Vertical");
		
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

		//Checking angles
		//Reduce speed if moving backward
		ChangeStat(ref curSpeed, curSpeed / 2.0f, DifferenceAngle(moveDirection) >= maxAngleDifference);

		Debug.Log(rigidBody.velocity);

		//Move
		Move(moveDirection);

		//anim.SetFloat("horizontal",moveSide * GlobalStatic.Mapping(curSpeed, 0, baseSpeed, 0, 1));
		//anim.SetFloat("vertical", moveForward * GlobalStatic.Mapping(curSpeed, 0, baseSpeed, 0, 1));
		anim.SetFloat("moveSpeed", rigidBody.velocity.magnitude);
		anim.SetBool("isRunning", isRunning);
	}
	private void Update()
	{
		//Shoot
		if (Input.GetButton("Fire1") == true)
		{
			curWeapon.UseWeapon();
		}
		//if (Input.GetButtonDown("Next Weapon") == true)
		//{
		//	ChangeWeapon();
		//}
		//ToDO: Change tool
		if (Input.GetButtonDown("Reload") == true)
		{
			curWeapon.Reload();
		}
		//Interact
		if (Input.GetMouseButtonDown(1))
		{
			InteractWithObject();
		}
	}

	#region Action
	public void ChangeWeapon()
	{
		//Add animation and icon
		int indx = weaponList.IndexOf(curWeapon);
		indx = indx >= weaponList.Count ? 0 : indx++;
		curWeapon = weaponList[indx];
	}
	#endregion

	#region Misc
	Vector3 GetMoveDirection(float moveSide, float moveForward)
	{
		//Input must be relative to camera rotation
		Transform cameraParent = Camera.main.transform.parent;
		Vector3 cameraForward = cameraParent.forward;
		Vector3 cameraRight = cameraParent.right;

		Vector3 moveX = cameraRight * moveSide;
		Vector3 moveZ = cameraForward * moveForward;
		return moveX + moveZ;
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
		else //When holding Right mouse button
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit, LayerMask.GetMask("Ground"));

            Vector3 _faceDirection = hit.point - transform.position;
			_faceDirection.y = 0;

			//Vector3 mouseLocation = Input.mousePosition;
			//Vector3 playerToMousePos2D = mouseLocation - Camera.main.WorldToScreenPoint(transform.position);

			//Vector3 faceDirection = new Vector3(playerToMousePos2D.x, 0, playerToMousePos2D.y);

			RotateToDirection(_faceDirection);

			
		}
	}

    public float DifferenceAngle(Vector3 moveDirection)
    {
        return Vector3.Angle(moveDirection, transform.forward);
    }
	#endregion

	void InteractWithObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Physics.Raycast(ray, out hit);

		Interactable interactable = hit.collider.GetComponent<Interactable>();

		if (interactable != null)
		{
			float distance = (transform.position - interactable.transform.position).magnitude;
			if (distance > interactable.radius)
				Debug.Log("Out of range!!");
			else
			{
				interactable.Interact();
			}
		}
	}
	
}