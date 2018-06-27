using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour {

    public GeneralObject target;

    public void TestDie()
    {
        Debug.Log("Target killed");
        target.Die();
    }
}
