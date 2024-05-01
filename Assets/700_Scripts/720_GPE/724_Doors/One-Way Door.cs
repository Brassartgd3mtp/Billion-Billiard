using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] List<Interrupteur> buttons;
    [SerializeField] private LockedDoor lockedDoor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallCollider;
    [SerializeField, Range(1, 5)] private float speed = 1;
    [SerializeField] private bool isOneWay;
    [SerializeField] private bool isOpenByDefault;
    [SerializeField] Vector3 undergroundEndPos = new Vector3(0, -1.5f, 0);
    private bool isOpen;
    Vector3 startPos;

    public Transform respawnPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = lockedDoor.transform.localPosition;

        if (isOpenByDefault)
            OpenDoor();
        else
            StartCoroutine(DoorState());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3 && isOpen && isOneWay)
            CloseDoor();
    }

    public void OpenDoor()
    {
        isOpen = true;
        StartCoroutine(UpdateDoorPos(lockedDoor.gameObject, undergroundEndPos));
        lockedDoor.Unlock = true;
        
        SoundDoorOpen();
    }

    void CloseDoor()
    {
        wallCollider.SetActive(true);

        isOpen = false;
        StartCoroutine(UpdateDoorPos(wall, startPos));
        enabled = false;

        PlayerController.posBeforeHit = new Vector3(respawnPos.position.x,PlayerController.posBeforeHit.y, respawnPos.position.z);;

        SoundDoorClose();
    }

    IEnumerator UpdateDoorPos(GameObject mesh, Vector3 endPos)
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

    IEnumerator DoorState()
    {
        yield return new WaitForSeconds(.2f);
        
        int buttonsEnabled = 0;

        foreach (Interrupteur button in buttons)
        {
            if (button.ContactPNJ)
                buttonsEnabled++;
        }

        if (buttonsEnabled == buttons.Count)
            OpenDoor();
        else
            StartCoroutine(DoorState());

        yield break;
    }

    private void SoundDoorOpen()
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        AudioManager.Instance.PlaySound(28, audioSource);
    }
    private void SoundDoorClose()
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        AudioManager.Instance.PlaySound(29, audioSource);
    }
}