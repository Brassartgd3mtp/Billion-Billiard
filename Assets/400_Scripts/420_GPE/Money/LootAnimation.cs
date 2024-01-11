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
    private float minSize = 0;
    private float timerSize = 0;

    public float scaleValue;

    private float timerMove;
    public float timerToMove;

    private Vector3 playerPos;
    private Vector3 startPos, endPos;


    private float rotateSpeed = 30;
    private Vector3 rotateDirection = new Vector3(0,1,0);

    public void Start()
    {
        player = PlayerCollisionBehavior.Instance.gameObject;
    }

    public void StartAnimation()
    {
        collider.enabled = false;
        transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);
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
