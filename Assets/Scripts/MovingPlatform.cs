using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject thePlatform;
    public GameObject thePlayer;

    void OnTriggerEnter(Collider other)
    {
        thePlayer.transform.parent = thePlatform.transform;
    }

    void OnTriggerExit(Collider other)
    {
        thePlayer.transform.parent = null;
    }
}
