using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        // Assurez-vous que le Dropdown est assigné
        if (resolutionDropdown != null)
        {
            // Remplissez la liste des résolutions dans le Dropdown
            FillResolutionOptions();

            // Définissez la résolution initiale
            SetInitialResolution();
        }
    }

    void FillResolutionOptions()
    {
        // Effacez les options actuelles du Dropdown
        resolutionDropdown.ClearOptions();

        // Obtenez la liste des résolutions disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Créez une liste de chaînes pour stocker les options du Dropdown
        var options = new List<TMP_Dropdown.OptionData>();

        // Remplissez la liste des options avec les résolutions
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
        // Définissez la résolution initiale comme la résolution actuelle de l'écran
        resolutionDropdown.value = FindCurrentResolutionIndex();
        resolutionDropdown.RefreshShownValue();
    }

    int FindCurrentResolutionIndex()
    {
        // Obtenez la résolution actuelle de l'écran
        Resolution currentResolution = Screen.currentResolution;

        // Obtenez la liste des résolutions dans le Dropdown
        Resolution[] resolutions = Screen.resolutions;

        // Recherchez l'index de la résolution actuelle dans la liste des résolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width &&
                resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        // Si la résolution actuelle n'est pas dans la liste, retournez 0 par défaut
        return 0;
    }

    public void OnResolutionChanged()
    {
        // Obtenez la résolution sélectionnée dans le Dropdown
        Resolution selectedResolution = Screen.resolutions[resolutionDropdown.value];

        // Appliquez la nouvelle résolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
