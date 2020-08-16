using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSResetPosition : MonoBehaviour
{
    public static LSResetPosition instance;

    public Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

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
        if(other.tag == "Player")
        {
            Player.instance.gameObject.SetActive(false);
            Player.instance.transform.position = respawnPosition;
            Player.instance.gameObject.SetActive(true);
        }
    }
}
