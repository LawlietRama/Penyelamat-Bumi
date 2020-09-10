using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel, levelSelect;

    public GameObject continueButton, howToPanel, creditPanel;

    public string[] levelNames;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Continue"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            ResetProgress();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("Continue", 0);
        PlayerPrefs.SetString("CurrentLevel", firstLevel);

        ResetProgress();
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetProgress()
    {
        for(int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
            PlayerPrefs.SetInt(levelNames[i] + "_stars", 0);

        }
        PlayerPrefs.SetInt("TotalStars", 0);
    }

    public void OpenHowTo()
    {
        howToPanel.SetActive(true);
    }

    public void CloseHowTo()
    {
        howToPanel.SetActive(false);
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredit()
    {
        creditPanel.SetActive(false);
    }
}
