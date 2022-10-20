using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public bool gameStarted;
    public bool gameover;

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
        gameStarted = false;
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        gameStarted = true;
        ScoreManager.instance.startGame();
        UiManager.instance.startGame();
        
    }
    public void gameOver()
    {
        gameover = true;
        ScoreManager.instance.gameover();
        UiManager.instance.gameover();
        
    }
}
