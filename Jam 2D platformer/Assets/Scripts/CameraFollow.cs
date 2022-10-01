using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float offset;
	public GameObject player;

    void Start()
    {
		
    }

    
    void LateUpdate()
    {
		Vector3 temp = transform.position;
		temp.x = player.transform.position.x;
		temp.x += offset;
		transform.position = temp;
    }
}
