 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;

    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public GameObject blackScreenObject, pauseScreen, optionsScreen, trashBinScreen, floatingJoystick, jumpButton, interactButton, pauseButton, levelEndScreen;

    public Text trashText;
    public Text starText;

    public Text levelEndTrash, levelEndStar, levelEndDeath;

    public Slider musicVolSlider, sfxVolSlider;

    public string levelSelect, mainMenu;

    public bool sampahPas;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.totalStars = PlayerPrefs.GetInt("TotalStars");
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(levelSelect))
        {
            starText.text = "" + GameManager.instance.totalStars;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
            blackScreenObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        
        }
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)             
            {
                fadeFromBlack = false;
                blackScreenObject.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1;
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
