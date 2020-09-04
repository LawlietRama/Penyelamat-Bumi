using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //public ButtonManager pauseButton;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int levelEndMusic = 8;

    public string levelToLoad;

    public bool isRespawning;

    public int currentTrashes;
    public int currentCertainTrashes;
    public int currentStars;
    public int totalStars;
    public int currentDeaths;

    public bool wasInteractActive = false;

    [Header("Ketika melanggar lalu lintas")]
    public int currentViolateTraffic;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/

        respawnPosition = Player.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))       //input.getbuttondowncancel
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        currentDeaths += 1;
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        Player.instance.gameObject.SetActive(false);

        CameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        Instantiate(deathEffect, Player.instance.transform.position + new Vector3 (0,2,0), Player.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        isRespawning = true;

        HealthManager.instance.ResetHealth();

        UIManager.instance.fadeFromBlack = true;

        Player.instance.transform.position = respawnPosition;

        CameraController.instance.theCMBrain.enabled = true;

        Player.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("CHECKPOINT");
    }

    public void AddTrashes(int trashesToAdd)
    {
        currentTrashes += trashesToAdd;
        UIManager.instance.trashText.text = "" + currentTrashes;
    }

    public void AddCertainTrashes(int trashesToAdd)
    {
        currentCertainTrashes += trashesToAdd;
    }

    public void AddViolate()
    {
        currentViolateTraffic += 1;
    }

    public void PauseUnpause()
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            UIManager.instance.floatingJoystick.SetActive(true);
            
            if (wasInteractActive == false)
            {
                UIManager.instance.jumpButton.SetActive(true);
            }
            else if (wasInteractActive)
            {
                UIManager.instance.interactButton.SetActive(true);
            }
            //pauseButtonObject.SetActive(true);
            Time.timeScale = 1f;

            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.floatingJoystick.SetActive(false);
            if (UIManager.instance.jumpButton.activeSelf)
            {
                wasInteractActive = false;
                UIManager.instance.jumpButton.SetActive(false);
            }
            else if (UIManager.instance.interactButton.activeSelf)
            {
                wasInteractActive = true;
                UIManager.instance.interactButton.SetActive(false);
            }
            //pauseButtonObject.SetActive(false);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void TrashBinPauseUnPause()
    {
        if (UIManager.instance.trashBinScreen.activeInHierarchy)
        {
            UIManager.instance.trashBinScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.trashBinScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        Player.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        
        UIManager.instance.levelEndScreen.SetActive(true);
        UIManager.instance.levelEndTrash.text = "" + currentTrashes;
        UIManager.instance.levelEndStar.text = "" + currentStars;
        UIManager.instance.levelEndDeath.text = "" + currentDeaths;

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        /*if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_trashes"))
        {
            if(currentTrashes > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_trashes"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_trashes", currentTrashes);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_trashes", currentTrashes);
        }*/

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_stars"))
        {
            if (currentStars > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_stars"))
            {
                Debug.Log("TOTAL STAR SEBELUM" + totalStars);
                Debug.Log("Star sebelum" + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_stars"));
                totalStars += currentStars - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_stars");
                //totalStars -= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_stars");
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_stars", currentStars);
                PlayerPrefs.SetInt("TotalStars", totalStars);
                Debug.Log("Star sekarang" + currentStars);
                Debug.Log("TOTAL STAR SEKARANG" + totalStars);

            }

        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_stars", currentStars);
            totalStars += currentStars;
            PlayerPrefs.SetInt("TotalStars", totalStars);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_deaths"))
        {
            if (currentDeaths > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_deaths"))
            {
                //totalStars -= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_stars");
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_deaths", currentDeaths);
            }

        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_deaths", currentDeaths);
        }

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
