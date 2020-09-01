using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonArea : MonoBehaviour
{
    public bool isPressed;
   

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPressed = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPressed = false;
        }
    }


}
