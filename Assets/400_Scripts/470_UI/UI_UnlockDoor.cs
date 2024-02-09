using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UnlockDoor : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        targetPosition = new Vector3(200, 45);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 formPosition = Camera.main.transform.position;
        formPosition.z = 0f;
        Vector3 dir = (toPosition - formPosition).normalized;
    }
}
