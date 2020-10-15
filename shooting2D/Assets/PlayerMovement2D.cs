using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool onGround = false;
    private bool m_FacingRight = true;
    public float speed = 10f;
    public float JumpForce = 20f;
    float move = 0;

    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        move = Input.GetAxisRaw("Horizontal");

        if (move > 0 && !m_FacingRight)
        {
            Flip();
            m_FacingRight = true;
        }

        else if (move < 0 && m_FacingRight)
        {
            Flip();
            m_FacingRight = false;
        }

        rb.velocity = new Vector2(move * speed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(new Vector2(0 , JumpForce));
            onGround = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "Ground")
        {
            onGround = true;
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}

