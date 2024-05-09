using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneSkip : MonoBehaviour
{
    CutscenesHandler cutscenesHandler;

    private void Start()
    {
        cutscenesHandler = GetComponent<CutscenesHandler>();
        InputHandler.CutscenesEnable(this);
    }

    internal void Skip(InputAction.CallbackContext context)
    {
        if (cutscenesHandler.TextBox.text == cutscenesHandler.CurrentDialogue)
        {
            CutscenesCurrent.CutsceneIndex += 1;
            StartCoroutine(cutscenesHandler.LoadCutscene(CutscenesCurrent.PackIndex, CutscenesCurrent.CutsceneIndex));
        }
        else
            cutscenesHandler.TextBox.text = cutscenesHandler.CurrentDialogue;
    }

    private void OnDisable()
    {
        InputHandler.CutscenesDisable();
    }
}
