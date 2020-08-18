using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrampoline : MonoBehaviour
{
    public float bounceForceY = 8f;
    public float bounceForceX = 8f;
    public float bounceForceZ = 8f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.instance.bounceMovement = new Vector3(bounceForceX, bounceForceY, bounceForceZ);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.instance.bounceMovement = Vector3.zero;
        }
    }
}
