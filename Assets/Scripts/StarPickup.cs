using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPickup : MonoBehaviour
{
    public GameObject inActive;
    public GameObject active;

    public GameObject effect;

    public Text starText;
    public GameObject image;

    [Space]
    [Header("Syarat umum")]
    public bool general;

    [Space]
    [Header("Syarat adalah sampah")]
    public bool trash;
    public int maxTrash;

    [Space]
    [Header("Syarat adalah tidak melanggar lalu lintas")]
    public bool violateTrafficLaw;
    public int maxViolate;

    


    // Start is called before the first frame update
    void Start()
    {
        if (general == true)
        {
            inActive.SetActive(false);
            active.SetActive(true);
            image.SetActive(false);
            //starText.transform.position = new Vector3(0, 0, 0);
            starText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trash == true)
        {
            starText.text = "" + GameManager.instance.currentTrashes + "/" + maxTrash;
            if (GameManager.instance.currentTrashes >= maxTrash && active.activeSelf == false)
            {
                inActive.SetActive(false);
                active.SetActive(true);
            }
        }
        else if (violateTrafficLaw == true)
        {
            starText.text = "" + GameManager.instance.currentViolateTraffic + "/" + maxViolate;
            if (GameManager.instance.currentViolateTraffic <= maxViolate && active.activeSelf == false)
            {
                inActive.SetActive(false);
                active.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && active.activeInHierarchy == true)
        {
            GameManager.instance.currentStars += 1;
            UIManager.instance.starText.text = "" + GameManager.instance.currentStars;
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}