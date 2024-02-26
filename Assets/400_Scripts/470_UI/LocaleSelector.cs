using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings; 



public class LocaleSelector : MonoBehaviour
{

    private bool active = false;


    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return; 


        StartCoroutine(SetlLocale(localeID));

    }



   IEnumerator SetlLocale(int _LocalID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_LocalID];
        active = false;
    }


}
