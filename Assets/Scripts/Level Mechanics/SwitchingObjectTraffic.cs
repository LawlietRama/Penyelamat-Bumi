using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingObjectTraffic : MonoBehaviour
{
    public GameObject theObject;

    public OnOffPlatform trafficLamp;

    public bool revealWhenPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trafficLamp.platformOn.activeSelf == true)
        {
            theObject.SetActive(revealWhenPressed);
        }
        else
        {
            theObject.SetActive(!revealWhenPressed);
        }
    }
}
