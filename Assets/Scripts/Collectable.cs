using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject pickupEffect;
    public int soundToPlay;
    public int value = 1;
    public bool isCertain = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //nambah skor
            GameManager.instance.AddTrashes(value);

            if(isCertain)
            {
                GameManager.instance.AddCertainTrashes(value);
            }

            //ngilangin objek
            Destroy(this.gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }

    private void Update()
    {
        
    }

}
