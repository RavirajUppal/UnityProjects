using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject TitlePanel;
    public GameObject GameoverPanel;
    public GameObject tapText;
    public Text score;
    public Text highscore1;
    public Text highscore2;
    public GameObject IngameScorePanel;
    public Text ingamescore;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore1.text = "Highscore : " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ingamescore.text = ScoreManager.instance.score.ToString();
    }

    public void startGame()
    {
        //tapText.GetComponent<Animator>().Play("TapDown");
        TitlePanel.GetComponent<Animator>().Play("PanelUp");
        tapText.SetActive(false);
        IngameScorePanel.SetActive(true);
    }
    public void gameover()
    {
        GameoverPanel.SetActive(true);
        score.text = ScoreManager.instance.score.ToString();
        highscore2.text = PlayerPrefs.GetInt("Highscore").ToString();
        IngameScorePanel.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
