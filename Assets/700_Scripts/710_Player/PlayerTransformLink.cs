using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformLink : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + 1.5f, 4, player.position.z);
    }
}
