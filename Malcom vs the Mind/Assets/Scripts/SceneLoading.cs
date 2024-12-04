using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    private int _curScene;

    private void Start()
    {
        _curScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }    

    public void ReloadScene()
    {
        SceneManager.LoadScene(_curScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
