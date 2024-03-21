using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputHandler))]
public class InputEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Box("Ce component est automatique. Ne pas toucher.");
    }
}