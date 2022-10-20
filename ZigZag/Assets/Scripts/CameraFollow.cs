using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    Vector3 campos;
    Vector3 offset;
    [SerializeField]
    float lerprate;
    public bool gameover;

    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        offset = ball.transform.position - transform.position;
        //lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            follow();
        }
    }

    void follow()
    {
        campos = transform.position;
        Vector3 targetpos = ball.transform.position - offset;
        transform.position = Vector3.Lerp(campos, targetpos, lerprate * Time.deltaTime);
        
    }
}
