using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public GameObject particle;
    [SerializeField]
    float speed;
    bool started;
    Rigidbody rb;
    bool gameover;
    [SerializeField]
    GameObject spawner;
    //platformSpawner platformSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        //platformSpawn =  spawner.GetComponent<platformSpawner>();
        //platformSpawn = GetComponent<platformSpawner>();
        rb = gameObject.GetComponent<Rigidbody>();
        started = false;
        gameover = false;
        

    }

    // Update is called once per frame
    void Update()
    {   
        if (!gameover && started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switchDirection();
            }
        }

        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(startGame());
            }
        }

        if(!Physics.Raycast(transform.position, Vector3.down , 1f))
        {
            rb.velocity = new Vector3(0, -20f, 0);
            gameover = true;
            Camera.main.GetComponent<CameraFollow>().gameover = true;
            //spawner.GetComponent<platformSpawner>().gameover = true;
            GameManagerScript.instance.gameOver();
            Destroy(gameObject, 2f);

        }
    }

    void switchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "diamond")
        {
            GameObject part = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
            Destroy(part, 1f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator startGame()
    {
        //spawner.GetComponent<platformSpawner>().gameStarted = true;
        
        
        GameManagerScript.instance.startGame();
        started = true;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector3(0, 0, speed);

    }
}
