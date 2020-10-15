using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class ClientPlayerMovement : MonoBehaviour
{
    public GameObject leftPaddle;
    Transform leftPaddleTransform;

    public GameObject rightPaddle;
    Transform rightPadddleTransform;

    public Transform ball;

    public GameObject UIPanel;

    Transform player;
    Rigidbody2D playerRb;

    Transform player2;
    Rigidbody2D player2Rb;
    float player2NewPos;

    public float speed = 5f;
    float timer;

    public float UBound;
    public float LBound;

    private SocketIOComponent socket;
    public GameObject go;


    void Start()
    {
        socket = go.GetComponent<SocketIOComponent>();
        player = leftPaddleTransform;
        playerRb = leftPaddle.GetComponent<Rigidbody2D>();


        leftPaddleTransform = leftPaddle.GetComponent<Transform>();
        rightPadddleTransform = rightPaddle.GetComponent<Transform>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Vertical");
        timer += Time.deltaTime;

        if (Mathf.Abs(input) > 0.1)
        {
            if (player.position.y >= UBound)
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

            //socket.Emit((player.transform.position.y).ToString());
            //Dictionary<string, string> data = new Dictionary<string, string>();
            //data["Position"] = (player.transform.position.y).ToString();
            //socket.Emit("beep", new JSONObject(data));

            Data data = new Data("Position", player.transform.position.y);

            string s = JsonUtility.ToJson(data);

            socket.Emit("player2Data", new JSONObject(s));
            //ws://127.0.0.1:3000/socket.io/?EIO=4&transport=websocket
        }

        if (input == 0)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            //socket.Off("boop", TestBoop);

            // player2Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //edited
        //if (Mathf.Abs(player2.position.y - player2NewPos) < 1.0)
        //{
        //    socket.Off("boop", TestBoop);
        //    Debug.Log("off" + Mathf.Abs(player2.position.y - player2NewPos));
        //}

        //if (timer >= 1 || Mathf.Abs(player2.position.y - player2NewPos) > 1.0)
        //{
        //    socket.On("boop", TestBoop);
        //    Debug.Log("on");
        //    timer = 0f;
        //}

        if (timer >= 1)
        {
            socket.OnAnother("player1Data", TestBoop);
            // Debug.Log("on");
            timer = 0f;
        }

        //if (Vector3.Distance(player2.position, ball.position) < 3)
        //{
        //    socket.OnAnother("boop", TestBoop);
        //    timer = 0f;
        //}
    }

    public void initilizePlayer1()
    {
        if (player == null)
        {
            player = leftPaddleTransform;
            playerRb = leftPaddle.GetComponent<Rigidbody2D>();
            UIPanel.SetActive(false);
        }

        if (player2 == null)
        {
            player2 = rightPadddleTransform;
            player2Rb = rightPaddle.GetComponent<Rigidbody2D>();
            Debug.Log("player 2 initialised");
            UIPanel.SetActive(false);
        }
    }

    public void initilizePlayer2()
    {
        if (player == null)
        {
            player = rightPadddleTransform;
            playerRb = rightPaddle.GetComponent<Rigidbody2D>();
            UIPanel.SetActive(false);
        }

        if (player2 == null)
        {
            player2 = leftPaddleTransform;
            player2Rb = leftPaddle.GetComponent<Rigidbody2D>();
            Debug.Log("player 2 initialised");
            UIPanel.SetActive(false);

        }
    }

    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("on");
        //Debug.Log(string.Format("[name: {0}, data: {1}]", e.name, e.data));
        Data s = JsonUtility.FromJson<Data>(e.data.ToString());
        if (player2.position.y != s.Position)
        {
            player2.position = new Vector3(player2.position.x, Mathf.Lerp(player2.position.y, s.Position, 0.1f), player2.position.z);
        }
        player2NewPos = s.Position;
        Debug.Log(s.Position);

    }
}

//[Serializable]
//public class Data
//{
//    string data;

//    public float Position;

//    public Data(string data, float position)
//    {
//        this.data = data;
//        Position = position;
//    }
//}
