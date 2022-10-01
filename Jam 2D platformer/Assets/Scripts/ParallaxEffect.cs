using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPos, length;
    public GameObject camera;
    public float parallaxEffectFactor;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {

        float distance = (camera.transform.position.x * parallaxEffectFactor);

        float xPos = startPos + distance;

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        float temp = (camera.transform.position.x * (1 - parallaxEffectFactor));
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if(temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
