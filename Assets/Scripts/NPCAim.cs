using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCAim : MonoBehaviour
{
    public Transform neck;
    public Transform to;
    public Transform toStart;

    void Start()
    {
        toStart = transform;
        to = toStart;
    }

    // Update is called once per frame
    void Update()
    {
        neck.transform.LookAt(to.transform, Vector3.up);
        neck.transform.rotation = Quaternion.Euler(0f, neck.transform.rotation.eulerAngles.y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            to = other.transform;


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            to = toStart;
    }

}
