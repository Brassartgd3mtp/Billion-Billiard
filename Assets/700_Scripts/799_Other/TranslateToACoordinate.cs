using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateToACoordinate : MonoBehaviour
{
    private Vector3 BaseTranslateLocation;
    private Vector3 TranslateLocation;
    private Vector3 offset;
    [SerializeField] private float speed;

    [SerializeField] private int xMin;
    [SerializeField] private int yMin;
    [SerializeField] private float xMax;
    [SerializeField] private int yMax;

    // Start is called before the first frame update
    private void Awake()
    {
        BaseTranslateLocation = transform.position;
        offset = new Vector3(Random.Range(xMin,xMax), Random.Range(yMin, yMax), 0);

        TranslateLocation = BaseTranslateLocation + offset;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TranslateLocation, speed * Time.deltaTime); ;
    }
}
