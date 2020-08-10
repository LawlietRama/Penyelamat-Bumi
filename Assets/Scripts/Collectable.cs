using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject pickupEffect;
    public int soundToPlay;
    public int value = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //nambah skor
            ScoreCounter.score += value;

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
