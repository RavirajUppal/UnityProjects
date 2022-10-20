using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public int highscore;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //PlayerPrefs.SetInt("score", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseScore()
    {
        score += 1;
    }
    public void startGame()
    {
        InvokeRepeating("IncreaseScore", 1f, 0.2f);
    }
    public void gameover()
    {
        CancelInvoke("IncreaseScore");
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if(score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
