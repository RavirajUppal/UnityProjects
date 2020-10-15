using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float mousesensitivity = 10;
    public Transform playerBody;
    public Rigidbody player;
    public float speed = 10f;
    public Transform head;
    float xRotation = 0f;
    public float jumpForce = 5f;
    float maxHealth = 100f;
    float health;
    public Slider slider;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealthBar();       
        Cursor.lockState = CursorLockMode.Locked;
    }

    public float ReturnHealth()
    {
        return health;
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Object.Destroy(player);

    }

    void FixedUpdate()
    {
        slider.value = CalculateHealthBar();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        move *= (speed * Time.deltaTime);
        playerBody.transform.position += move;


        float mouseX = Input.GetAxis("Mouse X") * mousesensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mousesensitivity;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        head.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetButtonDown("Jump"))
        {
            player.AddForce(0f, jumpForce, 0f);
        }

    }

    float CalculateHealthBar()
    {
        return health / maxHealth;
    }
}
