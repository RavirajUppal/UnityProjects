using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public Transform Firepoint;
    float range = 100f;
    float damage = 20;
    public GameObject ImpactEffect;
    float ImpactForce = 20f;
    bool isGameOver = false;

    //private void Start()
    //{
    //    GameManager.instance.gameOver += GameOverr;
    //}

    //private void OnDisable()
    //{
    //    GameManager.instance.gameOver -= GameOverr;
    //}

    //private void GameOverr()
    //{
    //    isGameOver = true;
    //}

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !isGameOver)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(Firepoint.position, Firepoint.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }
            GameObject ImpactOb = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactOb, 2f);
        }
    }
}
