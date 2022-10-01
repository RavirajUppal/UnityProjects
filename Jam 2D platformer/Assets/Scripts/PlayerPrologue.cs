using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrologue : MonoBehaviour
{
    Vector2 move;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("Walk", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1f,0,0) * Time.deltaTime * speed;
    }
}
