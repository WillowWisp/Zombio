using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralObject {

	[Header("Status")]
	public float maxAngleDifference = 120;
	private bool isMoving = false;

	[Header("Weapon thingy")]
	public List<Weapon> toolBelt;
	public Weapon curWeapon;

	[Header("Thumb")]
	[SerializeField] private Transform weaponPoint;

	protected override void Awake()
	{
		base.Awake();
		if (toolBelt.Count > 0)
		{
			curWeapon = toolBelt[0];
		}
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
		if (Input.GetButton("Fire1") == true && curWeapon != null)
		{
			curWeapon.UseWeapon();
		}
		if (Input.GetButtonDown("Next Weapon") == true && toolBelt.Count > 0)
		{
			ChangeWeapon();
		}
		//ToDO: Change tool
		if (Input.GetButtonDown("Reload") == true && curWeapon != null)
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
		Debug.Log("Co bam ne!");
		Debug.Log("Count : " + toolBelt.Count);
		//Add animation and icon
		int indx = toolBelt.IndexOf(curWeapon);
		Debug.Log(indx);
		indx = indx >= toolBelt.Count - 1? 0 : indx + 1;
		curWeapon.gameObject.SetActive(false);			//Set inactive -----		TODO: Change model!
		curWeapon = toolBelt[indx];
		curWeapon.gameObject.SetActive(true);           //Set active ------			TODO: Change model!
		Debug.Log("SAU : " + indx);
		curWeapon.StopWeapon();
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
	public void AddWeapon(Weapon newWeapon)
	{
		newWeapon.transform.parent = weaponPoint;      //Set parent		
		newWeapon.transform.localPosition = Vector3.zero;
		newWeapon.transform.rotation = transform.rotation;

		toolBelt.Add(newWeapon);

		if (curWeapon != null)
		{
			newWeapon.gameObject.SetActive(false);
		}
		else
		{
			curWeapon = newWeapon;
		}
	}
}