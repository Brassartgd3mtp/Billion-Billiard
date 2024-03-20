using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ConvoyerBelt))]
public class ConvoyerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Box("La direction du convoyer correspond \u00e0 la direction de l'axe Z (fl\u00e8che bleu)");
    }
}
