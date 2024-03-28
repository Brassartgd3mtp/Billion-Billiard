using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] private LockedDoor lockedDoor;
    [SerializeField] private GameObject wall;
    [SerializeField, Range(1, 5)] private float speed = 1;
    [SerializeField] private bool isOneWay;
    [SerializeField] private bool isOpenByDefault;
    private bool isOpen;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = lockedDoor.transform.localPosition;

        if (isOpenByDefault)
            OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3 && isOpen && isOneWay)
            CloseDoor();
    }

    public void OpenDoor()
    {
        Vector3 underground = new Vector3(0, -1.5f, 0);
        isOpen = true;
        StartCoroutine(DoorState(lockedDoor.gameObject, underground));
        lockedDoor.Unlock = true;

        Debug.Log("Open");
    }

    void CloseDoor()
    {
        isOpen = false;
        StartCoroutine(DoorState(wall, startPos));
        enabled = false;
    }

    IEnumerator DoorState(GameObject mesh, Vector3 endPos)
    {
        Vector3 meshPos = mesh.transform.localPosition;

        while (meshPos != endPos)
        {
            meshPos = mesh.transform.localPosition;
            mesh.transform.localPosition = Vector3.MoveTowards(meshPos, endPos, Time.deltaTime * speed);
            yield return null;
        }
        yield break;
    }
}
