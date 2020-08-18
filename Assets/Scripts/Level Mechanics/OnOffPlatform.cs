using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatform : MonoBehaviour
{
    public GameObject platformOn;
    public GameObject platformOff;

    public float counterSet = 2f;
    public float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = counterSet;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0f)
        {
            if (platformOn.activeSelf == true)
            {
                platformOn.SetActive(false);
                platformOff.SetActive(true);
                counter = counterSet;
            }
            else if (platformOff.activeSelf == true)
            {
                platformOff.SetActive(false);
                platformOn.SetActive(true);
                counter = counterSet;
            }
        }
        
    }
}
