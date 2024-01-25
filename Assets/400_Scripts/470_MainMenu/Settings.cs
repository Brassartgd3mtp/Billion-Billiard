using UnityEngine;
using UnityEngine.UI;

public class GestionOptions : MonoBehaviour
{

    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("Changed Screen Mode"); 
    }


}

