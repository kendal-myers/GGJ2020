using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroUIEvents : MonoBehaviour
{
    public Text msgBox;
    public Button nextBtn;

    public List<string> messages;
    public int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        if (messages.Count == 0) //skip if no intro
        {
            SceneManager.LoadScene("Asteroid Field", new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }

        nextBtn.Select();
        currentPage = 0;
        nextBtn.onClick.AddListener(GoNext);
        msgBox.text = messages[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoNext()
    {
        int nextPage = currentPage + 1;

        if (nextPage > messages.Count - 1)
        {
            SceneManager.LoadScene("Asteroid Field", new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }

        if (nextPage == messages.Count - 1)
        {
            nextBtn.GetComponentInChildren<Text>().text = "Launch ->";
        }

        msgBox.text = messages[nextPage];

        currentPage = nextPage;
    }
}
