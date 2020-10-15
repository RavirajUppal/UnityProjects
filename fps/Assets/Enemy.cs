using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform Player;
    private Rigidbody rb;
    public float speed = 5f;
    float maxHealth = 100f;
    float health;
    public GameObject HealthBar;
    public Slider slider;
    float EnemyDistance;
    public GameObject BulletPrefab;
    float NextTimeToFire = 0;
    float FireRate = 1;
    float EnemyDamage = 10f;
    private bool stop = false;
    

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealthBar();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        GameManager.instance.gameOver += GameOver;

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        GameManager.instance.gameOver -= GameOver;
    }

    private void GameOver()
    {
        Destroy(this);
    }

    void FixedUpdate()
    {
        Vector3 target = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        if(stop == false)
        {
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
            rb.MovePosition(newPos);
        }
        
        transform.LookAt(Player.position);
        
        slider.value = CalculateHealthBar();
        EnemyDistance = Vector3.Distance(target, rb.position);

        if(EnemyDistance <= 30 && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f/FireRate;
            attack();
            if (EnemyDistance <= 15)
            {
                stop = true;
            }
            else
            {
                stop = false;
            }
                
        }

    }

    float CalculateHealthBar()
    {
        return health / maxHealth;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void attack()
    {

        GameObject bulletOb = Instantiate(BulletPrefab, rb.position, rb.rotation);
        Destroy(bulletOb, 2f);

    }

}
