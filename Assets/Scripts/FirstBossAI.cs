using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstBossAI : MonoBehaviour
{
    public float speed;
    public Transform target;
    private float step, dist;

    private Vector3 startPos;

    public GameObject crates;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isRespawning)
        {
            transform.position = startPos;
            for (int a = 0; a < crates.transform.childCount; a++)
            {
                crates.transform.GetChild(a).gameObject.SetActive(true);
            }
            GameManager.instance.isRespawning = false;
        }


        dist = Vector3.Distance(target.position, transform.position);
        if(dist<23)
        {
            step = speed * Time.deltaTime; // calculate distance to move
        }
        else if(transform.position.z > 347)
        {
            step = 0;
        }
        else
        {
            step = (speed + 6) * Time.deltaTime;
        }
        
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), step);
    }
}
