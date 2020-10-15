using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;

    void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }          
    }

    void shoot()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
