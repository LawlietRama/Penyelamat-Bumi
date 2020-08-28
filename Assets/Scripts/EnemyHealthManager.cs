using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth = 1;
    private float currentHealth;

    public int deathSound;

    public GameObject deathEffect, itemToDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        Player.instance.Bounce();
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);

            Destroy(gameObject);

            

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(itemToDrop, transform.position + new Vector3(0,1,0), transform.rotation);
        }
    }
}
