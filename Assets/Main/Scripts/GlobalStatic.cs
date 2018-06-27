using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatic {

	public static Transform GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player").transform;
    }
}
