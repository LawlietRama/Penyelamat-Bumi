using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnEnable : MonoBehaviour
{
    public string levelName;

    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SkipScene();
    }

    public void SkipScene()
    {
        SceneManager.LoadScene(levelName);
    }
}
