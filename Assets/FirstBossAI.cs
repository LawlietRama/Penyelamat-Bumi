using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstBossAI : MonoBehaviour
{
    public float speed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), step);
    }
}
