using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralObject {

    private void FixedUpdate()
    {
        Transform cameraParent = Camera.main.transform.parent;

        float moveSide = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraParent.forward;
        Vector3 cameraRight = cameraParent.right;

        Vector3 moveDirection = new Vector3();

        Vector3 moveX = cameraRight * moveSide;
        Vector3 moveZ = cameraForward * moveForward;
        moveDirection = moveX + moveZ;

        //Matrix4x4 myMatrix = Matrix4x4.identity;
        //myMatrix.SetColumn(0, cameraParent.InverseTransformDirection(Vector3.right));
        //myMatrix.SetColumn(1, cameraParent.InverseTransformDirection(Vector3.up));
        //myMatrix.SetColumn(2, cameraParent.InverseTransformDirection(Vector3.forward));
        //moveDirection = myMatrix.MultiplyVector(new Vector3(moveSide, 0, moveForward));

        Move(moveDirection);
    }
}
