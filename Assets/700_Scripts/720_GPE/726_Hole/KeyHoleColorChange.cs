using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleColorChange : MonoBehaviour
{
    private MaterialPropertyBlock materialPropertyBlock;
    private MeshRenderer meshRenderer;
    private Interrupteur interrupteurScript;
    public Color MyColor;

    private void Start()
    {
        interrupteurScript = GetComponentInParent<Interrupteur>();
    }

    private void Update()
    {
        if (interrupteurScript.ChangeColor == true)
        {
            ChangeColor();
            enabled = false;
        }
    }
    private void ChangeColor()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(materialPropertyBlock);

        materialPropertyBlock.SetColor("_Color_01", MyColor);
        materialPropertyBlock.SetColor("_Color_02", MyColor);

        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
