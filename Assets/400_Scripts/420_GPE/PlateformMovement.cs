using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateformMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float waitingTime;
    private float resetTime;

    [SerializeField] private GameObject movePoint;

    [SerializeField] private List<GameObject> pointsToReach = new List<GameObject>();
    private Vector3 actualPosition;
    [SerializeField] private GameObject NextPointToReach;

    public enum NextPointOrder {Croissant, Decroissant, Aleatoire}

    public NextPointOrder nextPointOrder = NextPointOrder.Croissant;

    void Start()
    {
        resetTime = waitingTime;
        DefineNewPointToReach();
    }

    void Update()
    {
        // La plateforme va du point actuel, vers le point target à une "vitesse"
        transform.position = Vector3.MoveTowards(transform.position, NextPointToReach.transform.position, speed * Time.deltaTime);

        CheckIfPositionReached();

    }

    public void CheckIfPositionReached()
    {
        if (Vector3.Distance(transform.position, NextPointToReach.transform.position) < 0.001f)
        {
            waitingTime -= Time.deltaTime;
        }

        // Si le point target est reach, attente d'un certain nomnbre de temps 
        if (waitingTime <= 0)
        {
            DefineNewPointToReach();
        }
    }

    public void DefineNewPointToReach()
    {
        waitingTime = resetTime;
        actualPosition = transform.position;

        switch (nextPointOrder)
        {
            case NextPointOrder.Croissant:
                if (NextPointToReach == null)
                {
                    NextPointToReach = pointsToReach[0];
                }
                else
                {
                    int index = pointsToReach.IndexOf(NextPointToReach);
                    if ((index + 1) < pointsToReach.Count)
                    {
                        NextPointToReach = pointsToReach[index + 1];
                    }

                    else 
                    {
                        nextPointOrder = NextPointOrder.Decroissant;
                        DefineNewPointToReach();
                    }
                }
                break;
            
            case NextPointOrder.Decroissant:
                if (NextPointToReach == null)
                {
                    NextPointToReach = pointsToReach[pointsToReach.Count - 1];
                }
                else
                {
                    
                    int index = pointsToReach.IndexOf(NextPointToReach);
                    if ((index - 1) >= 0) 
                    {
                    NextPointToReach = pointsToReach[index - 1];
                    }
                    else 
                    {
                        nextPointOrder = NextPointOrder.Croissant;
                        DefineNewPointToReach();
                    }
                }
                break;
        }
    }

    public void CreatePointForPlateform()
    {
        pointsToReach.Add(Instantiate(movePoint, transform.position, Quaternion.identity, transform.parent));
    }
}
