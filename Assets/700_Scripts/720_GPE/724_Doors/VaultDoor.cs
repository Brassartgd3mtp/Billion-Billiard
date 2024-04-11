using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    [SerializeField] List<Interrupteur> buttons;
    [SerializeField] private Animator DoorAnimator;
    public string SelectedAnimation;
    public bool NeedButton;

    void Start()
    {
        
    }


    void Update()
    {
        
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
            OpenVaultDoor();
        else
            StartCoroutine(DoorState());

        yield break;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!NeedButton && other.gameObject.layer == 3)
        {
            OpenVaultDoor();
        }
    }

    public void OpenVaultDoor()
    {
        DoorAnimator.Play(SelectedAnimation);
    }
}
