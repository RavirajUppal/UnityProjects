using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject Diamond;
    Vector3 lastpos;
    public bool gameStarted;
    public bool gameover;
    [SerializeField]
    float repeattime;
    bool startonce;
    bool cancelonce;
    float size;
    [SerializeField]
    float diamondHeight;

    // Start is called before the first frame update
    void Start()
    {
        startonce = true;
        cancelonce = true;
        gameover = false;
        lastpos = transform.position;
        size = platform.transform.localScale.x;
        for (int i = 0; i< 30; i++)
        {
            spawnPlatforms();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (startonce)
        {
            if (GameManagerScript.instance.gameStarted)
            {
                InvokeRepeating("spawnPlatforms", 1f, repeattime);
                startonce = false;
            }
        }
        if (cancelonce)
        {
            if (GameManagerScript.instance.gameover)
            {
                CancelInvoke("spawnPlatforms");
                cancelonce = false;
            }
        }
        
    }

    void spawnPlatforms()
    {
        int rand = Random.Range(0, 10);
        if(rand%2 == 0)
        {
            spawnX();
        }
        else
        {
            spawnZ();
        }
       
    }
    void spawnX()
    {
        Vector3 newPosition = lastpos;
        newPosition.x += size;
        Instantiate(platform, newPosition, Quaternion.identity);
        lastpos = newPosition;

        newPosition.y += diamondHeight;
        int rand = Random.Range(0, 4);
        if(rand < 1)
        {
            Instantiate(Diamond, newPosition, Diamond.transform.rotation);
        }
    }
    void spawnZ()
    {
        Vector3 newPosition = lastpos;
        newPosition.z += size;
        Instantiate(platform, newPosition, Quaternion.identity);
        lastpos = newPosition;

        newPosition.y += diamondHeight;
        int rand = Random.Range(0, 4);
        if (rand < 1)
        {
            Instantiate(Diamond, newPosition, Diamond.transform.rotation);
        }
    }



}
