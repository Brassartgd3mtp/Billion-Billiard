using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool isOpen;
    Vector3 startPos;
    new BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        startPos = door.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _pc) && isOpen)
            CloseDoor();
    }

    public void OpenDoor()
    {
        isOpen = true;
        StartCoroutine(DoorState(-Vector3.forward));
        //door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, -Vector3.forward, Time.deltaTime);
    }

    void CloseDoor()
    {
        isOpen = false;
        StartCoroutine(DoorState(startPos));
        //door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, startPos, Time.deltaTime);
        enabled = false;
    }

    IEnumerator DoorState(Vector3 endPos)
    {
        Vector3 doorPos = door.transform.localPosition;

        while (doorPos != endPos)
        {
            doorPos = door.transform.localPosition;
            door.transform.localPosition = Vector3.MoveTowards(doorPos, endPos, Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}
