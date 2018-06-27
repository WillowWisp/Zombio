using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GeneralObject {

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        Move(moveDirection);
    }
}
