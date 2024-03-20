using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        // Assurez-vous que le Dropdown est assign�
        if (resolutionDropdown != null)
        {
            // Remplissez la liste des r�solutions dans le Dropdown
            FillResolutionOptions();

            // D�finissez la r�solution initiale
            SetInitialResolution();
        }
    }

    void FillResolutionOptions()
    {
        // Effacez les options actuelles du Dropdown
        resolutionDropdown.ClearOptions();

        // Obtenez la liste des r�solutions disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Cr�ez une liste de cha�nes pour stocker les options du Dropdown
        var options = new List<TMP_Dropdown.OptionData>();

        // Remplissez la liste des options avec les r�solutions
        foreach (Resolution resolution in resolutions)
        {
            string optionText = resolution.width + "x" + resolution.height;
            options.Add(new TMP_Dropdown.OptionData(optionText));
        }

        // Ajoutez les options au Dropdown
        resolutionDropdown.AddOptions(options);
    }

    void SetInitialResolution()
    {
        // D�finissez la r�solution initiale comme la r�solution actuelle de l'�cran
        resolutionDropdown.value = FindCurrentResolutionIndex();
        resolutionDropdown.RefreshShownValue();
    }

    int FindCurrentResolutionIndex()
    {
        // Obtenez la r�solution actuelle de l'�cran
        Resolution currentResolution = Screen.currentResolution;

        // Obtenez la liste des r�solutions dans le Dropdown
        Resolution[] resolutions = Screen.resolutions;

        // Recherchez l'index de la r�solution actuelle dans la liste des r�solutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width &&
                resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        // Si la r�solution actuelle n'est pas dans la liste, retournez 0 par d�faut
        return 0;
    }

    public void OnResolutionChanged()
    {
        // Obtenez la r�solution s�lectionn�e dans le Dropdown
        Resolution selectedResolution = Screen.resolutions[resolutionDropdown.value];

        // Appliquez la nouvelle r�solution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
