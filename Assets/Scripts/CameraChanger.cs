using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject camBefore;
    public GameObject camAfter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            camBefore.SetActive(false);
            camAfter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        camBefore.SetActive(true);
        camAfter.SetActive(false);
    }
}
