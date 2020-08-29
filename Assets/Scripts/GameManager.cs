using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int levelEndMusic = 8;

    public string levelToLoad;

    public int currentTrashes;
    public int currentCertainTrashes;
    public int currentStars;
    public int totalStars;
    public int currentDeaths;

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
        if(Input.GetButtonDown("Cancel"))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        Player.instance.gameObject.SetActive(false);

        CameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        Instantiate(deathEffect, Player.instance.transform.position + new Vector3 (0,2,0), Player.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

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
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.floatingJoystick.SetActive(false);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        Debug.Log("Level Ended");

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

        SceneManager.LoadScene(levelToLoad);

    }

}
