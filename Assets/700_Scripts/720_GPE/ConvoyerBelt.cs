using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField, Range(0, 5), Space] private float speedTexture;
    [SerializeField, Range(1, 50)] float applyForce = 10;
    [SerializeField] private Vector2 Tiling;
    [SerializeField] private Vector2 Direction;
    private MaterialPropertyBlock materialPropertyBlock;
    private float rotateTexture;
    private Vector3 forceDirection;

    private void Start()
    {
        InitPropertyBlock();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody _colrb))
            _colrb.AddForce(transform.TransformDirection(forceDirection) * applyForce);
    }

    private void InitPropertyBlock()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        rotateTexture = Vector2.SignedAngle(Vector2.right, Direction);
        forceDirection = new Vector3(Direction.x, 0, Direction.y).normalized;
        
        materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        
        materialPropertyBlock.SetFloat("_Rotate", rotateTexture);
        materialPropertyBlock.SetVector("_Tiling", Tiling);
        materialPropertyBlock.SetVector("_Speed", Vector2.right * speedTexture);
        
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        InitPropertyBlock();
    }
#endif
}