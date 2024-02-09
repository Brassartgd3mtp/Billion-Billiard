using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlateformMovement)), CanEditMultipleObjects]
public class PlateformMovementInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlateformMovement plateformMovement = (PlateformMovement)target;
        if (GUILayout.Button("Create A Point"))
        {
            plateformMovement.CreatePointForPlateform();
        }
    }
}
