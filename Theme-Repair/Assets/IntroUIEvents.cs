using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class IntroUIEvents : MonoBehaviour
{
    public Text msgBox;
    public Image imageBox;
    public Button nextBtn;
    
    public List<PicNText> pages;

    private int currentPage;
    private Text nextText;

    // Start is called before the first frame update
    void Start()
    {
        if (pages.Count == 0) //skip if no intro
        {
            SceneManager.LoadScene("Asteroid Field", new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }

        nextBtn.Select();
        nextText = nextBtn.GetComponentInChildren<Text>();
        currentPage = 0;
        msgBox.text = pages[0].text;
        imageBox.sprite = pages[0].image;
        nextText.text = pages[0].nextText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNext()
    {
        int nextPage = currentPage + 1;

        if (nextPage > pages.Count - 1)
        {
            SceneManager.LoadScene("Asteroid Field", new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }

        msgBox.text = pages[nextPage].text;
        imageBox.sprite = pages[nextPage].image;
        nextText.text = pages[nextPage].nextText;

        currentPage = nextPage;
    }

    [System.Serializable]  
    public class PicNText
    {
        public string text;
        public Sprite image;
        public string nextText;
    }
}
