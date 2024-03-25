using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrupteurSpeedText : MonoBehaviour
{
    public string ParameterName = "_Speed"; // Nom de la propri�t� dans le shader
    public Vector2 NewSpeed = new Vector2(0, 0); // Nouvelle valeur que vous souhaitez attribuer � la propri�t�

    public MeshRenderer MyMeshRenderer;

    public Interrupteur interrupteurScript;
    void Start()
    {
        MyMeshRenderer = GetComponent<MeshRenderer>();
        interrupteurScript = GetComponentInParent<Interrupteur>();
        if (MyMeshRenderer == null)
        {
            Debug.LogError("Le composant Renderer n'a pas �t� trouv�.");
            return;
        }
    }
    private void Update()
    {
        if (interrupteurScript.ContactPNJ == true)
        {
            Material materialInstance = MyMeshRenderer.material;
            materialInstance.SetVector(ParameterName, NewSpeed);
        }
    }
}