using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{


    [SerializeField] private EventSystem eventController;

    [SerializeField] private GameObject selectedGameObject; 


    public void SetSelectedGameObject(GameObject _element)
    {
        eventController.SetSelectedGameObject(_element); 
    }

    private MenuController controller; 


    public void Init(MenuController _controller) { controller = _controller; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
