using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class BallMovement : MonoBehaviour
{
    private SocketIOComponent socket;
    //public GameObject go;

    Rigidbody2D ballRb;
    Vector3 ogPos;
    Vector2 velocity = new Vector2(5, 5); 

    void Start()
    {
        //socket = go.GetComponent<SocketIOComponent>();


        ballRb = GetComponent<Rigidbody2D>();
        ogPos = gameObject.transform.position;
        ballRb.velocity = velocity;
    }

    void Update()
    {
        ballRb.velocity = velocity;

    }

    void resetPos()
    {
        transform.position = ogPos;
        velocity = new Vector2(5, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "UpperBound" || collision.gameObject.tag == "LowerBound")
        {
            velocity.y *= -1;
            ballRb.velocity = new Vector2(velocity.x, velocity.y);
        }

        if(collision.gameObject.tag == "RightPaddle" || collision.gameObject.tag == "LeftPaddle")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            velocity.x *= -1;
            ballRb.velocity = new Vector2(velocity.x, velocity.y);
        }

        if (collision.gameObject.tag == "Out")
            resetPos();
    }
}
