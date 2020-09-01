using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingObjects : MonoBehaviour
{
    public GameObject theObject;

    public bool isButtonGeneral = true;

    public ButtonController theButton;

    public ButtonArea theButtonArea;

    private Vector3 startPosition;

    public bool revealWhenPressed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = theObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isButtonGeneral)
        {
            if (theButton.isPressed)
            {
                theObject.SetActive(revealWhenPressed);
            }
            else
            {
                theObject.SetActive(!revealWhenPressed);
            }
        }
        else // this is button area, where player must keep still so that the button ispressed
        {
            if (theButtonArea.isPressed)
            {
                theObject.transform.position = startPosition;
                theObject.SetActive(revealWhenPressed);
            }
            else
            {
                theObject.SetActive(!revealWhenPressed);
            }
        }
        
    }
}
