using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public GameObject EnemyPrefab;
    public delegate void GameOver();
    public GameOver gameOver;

    public PlayerScript player;
    
    int spawnValue = 0;
    private float count = 10;
    //private float spawn = 0f;


    void Start()
    {
        instance = this;
        //InvokeRepeating("EnemySpawn" , 1f, 5f);
                 
    }

    private void Update()
    {

        count -= Time.deltaTime;
        if(count < 0)
        {
            //spawnValue++;
            EnemySpawn();
            count = 8f;
        }

        if(player.ReturnHealth() < 0)
        {
            gameOver();
        }
        
    }

    public void EnemySpawn()
    {

        if (spawnValue == 1)
        {
            Instantiate(EnemyPrefab, spawnpoint2.position, spawnpoint2.rotation);
            spawnValue = 0;
        }

        if (spawnValue == 0)
        {
            Instantiate(EnemyPrefab, spawnpoint1.position, spawnpoint1.rotation);
            spawnValue = 1;
        }

    }
}
