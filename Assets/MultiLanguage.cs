using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;
using Assets.SimpleLocalization.Scripts;

public class MultiLanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        LocalizationManager.Language = "English";

    }


}
