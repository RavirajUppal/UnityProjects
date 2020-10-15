using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody rb;
    float EnemyDamage = 20f;


    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        PlayerScript player = hitInfo.transform.GetComponent<PlayerScript>();
        if (player != null)
        {
            //Debug.Log("cvfyjtvvvv = " + hitInfo.transform.name);
            player.takeDamage(EnemyDamage);
        }
    }


}
