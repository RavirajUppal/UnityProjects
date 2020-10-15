using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

enum GameStates
{
    PlayerInit,
    GameStarted,
    PointScored
}


public class PlayerMovementOptimised : MonoBehaviour
{
    #region Player Select
    public GameObject leftPaddle;
    public GameObject rightPaddle;

    GameObject player;
    GameObject opponent;

    public GameObject UIPanel;
    #endregion


    #region Gameplay
    float P2CurrentPos;
    float P2ONewPos;

    float timer = 0f;
    bool refresh = false;
    bool updatePos = false;


    public float speed = 5f;

    public float UBound;
    public float LBound;
    public Transform LeftBound;
    public Transform RightBound;
    #endregion

    GameStates gameState;

    public Transform ball;

    private SocketIOComponent socket;
    public GameObject SocketIO;

    void Start()
    {
        gameState = GameStates.PlayerInit;

        socket = SocketIO.GetComponent<SocketIOComponent>();

        //Debug.Log(Vector3.Distance(ball.position, LeftBound.position));
        //Debug.Log(Vector3.Distance(ball.position, RightBound.position));

        playerInit();

        socket.On("P2Data", receiveData);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Vector3.Distance(ball.position, LeftBound.position));
            Debug.Log(Vector3.Distance(ball.position, RightBound.position));
        }

        if (gameState == GameStates.GameStarted)
        {
            //Player side movemnet
            float input = Input.GetAxis("Vertical");

            if (Mathf.Abs(input) > 0.1)
            {
                if (player.transform.position.y >= UBound)
                {
                    if (input < -0.1)
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (input * speed), player.transform.position.z);
                }
                else if (player.transform.position.y <= LBound)
                {
                    if (input > 0.1)
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (input * speed), player.transform.position.z);
                }
                else
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (input * speed), player.transform.position.z);

                timer = 0;
                refresh = true;
            }
            else if(input == 0)
            {
                timer += Time.deltaTime;

                if(timer >= 0.1 && refresh == true)
                {
                    sendData();
                }
            }

            //Opponent movement
            if(updatePos == true)
            {
                player2Movement();
            }

        }
    }

    #region Movement Data

    void player2Movement()
    {
        if(P2CurrentPos != P2ONewPos)
        {
            //Todo calcutions for getting the mean of the data

            //opponent.transform.Translate(P2ONewPos>P2CurrentPos? (Vector2.up * P2ONewPos) : (-Vector2.up * P2ONewPos));

            opponent.transform.position = Vector2.MoveTowards(new Vector2(opponent.transform.position.x, P2CurrentPos), new Vector2(opponent.transform.position.x, P2ONewPos), (speed * 10) * Time.deltaTime);
            P2CurrentPos = opponent.transform.position.y;
            Debug.Log(P2CurrentPos);
        }
        if(P2CurrentPos == P2ONewPos)
        {
            P2CurrentPos = P2ONewPos;
            updatePos = false;
        }
    }

    void sendData()
    {
        Data data = new Data("Position", player.transform.position.y);

        string s = JsonUtility.ToJson(data);
        socket.Emit("P1Data", new JSONObject(s));
        timer = 0f;
        refresh = false;
    }

    void receiveData(SocketIOEvent e)
    {
        Data s = JsonUtility.FromJson<Data>(e.data.ToString());

        if (s.Position >= P2CurrentPos + 1 || s.Position <= P2CurrentPos - 1)
        {
            P2ONewPos = s.Position;
            updatePos = true;
            //player2Movement(s.Position);
        }
    }
    #endregion

    #region Player Init Methods

    public void playerInit()
    {
        UIPanel.SetActive(true);
        Time.timeScale = 0;

        socket.On("P2SelectTrigger", Player2Select);
        socket.On("P1SelectTrigger", Player1Select);
    }

    public void Player1Select(SocketIOEvent e)
    {
        if (player == null)
        {
            player = leftPaddle;
            opponent = rightPaddle;
        }
        Debug.Log("Player1 Selected");
        UIPanel.SetActive(false);
        Time.timeScale = 1;
        gameState = GameStates.GameStarted;
    }

    public void Player2Select(SocketIOEvent e)
    {
        if (player == null)
        {
            player = rightPaddle;
            opponent = leftPaddle;
        }
        Debug.Log("Player2 Selected");
        UIPanel.SetActive(false);
        Time.timeScale = 1;
        gameState = GameStates.GameStarted;
    }

    // UI Functions
    public void InitPlayer1()
    {
        player = leftPaddle;
        opponent = rightPaddle;
        socket.Emit("Player1Selected");
        Debug.Log("Player1 Selected");
        UIPanel.SetActive(false);
        //Time.timeScale = 1;


        //TODO Set other client to player2 || Disable player1 button on other client
        //server will send player2 join request upoun this
    }

    public void InitPlayer2()
    {
        player = rightPaddle;
        opponent = leftPaddle;
        socket.Emit("Player2Selected");
        Debug.Log("Player2 Selected");
        UIPanel.SetActive(false);
        //Time.timeScale = 1;
    }
    #endregion
}

[Serializable]
public class Data
{
    string data;

    public float Position;

    public Data(string data, float position)
    {
        this.data = data;
        Position = position;
    }
}
