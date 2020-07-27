using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false)
            {
                Time.timeScale = 0;
                isPause = true;
            }
            else if (isPause == true)
            {
                Time.timeScale = 1;
                isPause = false;
            }
            
        }
    }
}
