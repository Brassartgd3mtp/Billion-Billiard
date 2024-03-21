using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject wall;
    private bool isOpen;
    Vector3 startPos;
    new BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        startPos = door.transform.localPosition;

        OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _pc) && isOpen)
            CloseDoor();
    }

    public void OpenDoor()
    {
        Vector3 underground = new Vector3(0, -1.5f, 0);
        isOpen = true;
        StartCoroutine(DoorState(door, underground));
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
            mesh.transform.localPosition = Vector3.MoveTowards(meshPos, endPos, Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}
