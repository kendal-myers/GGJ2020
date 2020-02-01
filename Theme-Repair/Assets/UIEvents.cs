using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    public Button startBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
    }


    void StartGame()
    {
        SceneManager.LoadScene("Intro", new LoadSceneParameters(LoadSceneMode.Single));
    }
    
}
