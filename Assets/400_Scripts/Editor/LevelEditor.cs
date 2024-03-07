using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    [MenuItem("Window/LevelEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    public GameObject actualGameObject;
    public GameObject lastGameObjectPutInScene;

    private SerializedProperty items;

    private void OnGUI()
    {
        GUILayout.Label("Selection", EditorStyles.boldLabel);
        actualGameObject = (GameObject)EditorGUILayout.ObjectField(" Selected GameObject", actualGameObject,typeof(GameObject), true);
        lastGameObjectPutInScene = (GameObject)EditorGUILayout.ObjectField(" Selected GameObject", lastGameObjectPutInScene, typeof(GameObject), true);

        if(GUILayout.Button("Instantiate Actual GameObject"))
        {
           Instantiate(actualGameObject);
        }

        
    }
}
