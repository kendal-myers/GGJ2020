using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Intro", new LoadSceneParameters(LoadSceneMode.Single));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
}
