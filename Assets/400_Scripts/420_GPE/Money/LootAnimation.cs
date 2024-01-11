using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LootAnimation : MonoBehaviour
{
    public GameObject player;
    public Collider collider;
    
    private bool isLooted;
    private bool abovePlayer;

    [SerializeField] float timeToScale;
    public float minSize = 0;
    public float timerSize = 0;

    public float timerMove;
    public float timerToMove;

    public Vector3 playerPos;
    public Vector3 startPos, endPos;


    private float rotateSpeed = 30;
    private Vector3 rotateDirection = new Vector3(0,1,0);


    public void StartAnimation()
    {
        collider.enabled = false;
        transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        StartCoroutine(MoveAbovePlayer());
        isLooted = true;
    }

    public void Update()
    {
        playerPos = player.transform.position;

        endPos = playerPos + new Vector3(-0, 1, 0);

        if (isLooted)
        {
            //transform.position = player.transform.position + new Vector3(0,1,0);

            transform.Rotate(rotateSpeed * rotateDirection * Time.deltaTime);
        }

        if (abovePlayer)
        {
            transform.position = endPos;
        }
    }

    private IEnumerator MoveAbovePlayer()
    {
        Vector3 startPos = transform.position;
        do
        {
            transform.position = Vector3.Lerp(startPos, endPos, timerMove / timerToMove);
            timerMove += Time.deltaTime;
            yield return null;
        }
        while (transform.position != endPos);

        StartCoroutine(Grow());
        abovePlayer = true;
    }

    private IEnumerator Grow()
    {
        Vector3 startSize = transform.localScale;
        Vector3 endSize = new Vector3(minSize, minSize, minSize);
    
        do
        {
            transform.localScale = Vector3.Lerp(startSize, endSize,timerSize / timeToScale);
            timerSize += Time.deltaTime;
            yield return null;
        }
        while (timerSize < timeToScale);
    
        Destroy(gameObject);
    }
}
