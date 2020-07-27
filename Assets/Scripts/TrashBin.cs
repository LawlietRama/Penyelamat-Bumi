using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Bisa bro");
            GameManager.instance.TrashBinPauseUnPause();
        }
        
    }
}
