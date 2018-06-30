using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

	//Add new child
    public void AddChild(Transform newChild)
    {
        if (newChild.parent != transform) {
            newChild.parent = transform;
        }
    }
}
