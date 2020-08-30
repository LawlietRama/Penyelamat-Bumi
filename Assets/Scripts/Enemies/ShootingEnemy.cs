using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject model;
    public float timeToRotate = 2f;
    public float rotationSpeed = 6f;

    public bool isRotate = true;
    public bool isLookPlayer = false;

    public GameObject bulletPrefab;
    public float timeToShoot = 1f;
    

    private int targetAngle;
    private float rotationTimer;
    private float shootingTimer;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        rotationTimer = timeToRotate;
        shootingTimer = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the enemy's angle
        if(isRotate)
        {
            rotationTimer -= Time.deltaTime;
            if (rotationTimer <= 0f)
            {
                rotationTimer = timeToRotate;
                targetAngle += 90;
            }
            // Perform rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * rotationSpeed);
        }
        else if(isLookPlayer)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, -direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, toRotation.y, 0), rotationSpeed * Time.deltaTime);
        }
        

        //Shoot bullets
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0f)
        {
            shootingTimer = timeToShoot;

            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = transform.position + model.transform.forward;
            bulletObject.transform.forward = -model.transform.forward;
        }
    }
}
