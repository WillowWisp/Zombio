using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestObject))]
public class TestObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestObject myScript = (TestObject)target;
        if (GUILayout.Button("Kill Object") == true) {
            myScript.TestDie();
        }

    }

}
