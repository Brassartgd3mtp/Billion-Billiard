using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroySpeed : MonoBehaviour
{
    [SerializeField] float destroyValue;
    [SerializeField] new BoxCollider collider;

    public void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.rb.velocity.magnitude > destroyValue)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (PlayerController.rb.velocity.magnitude < destroyValue)
        {
            collider.isTrigger = false;
        } else collider.isTrigger = true;
    }
}
