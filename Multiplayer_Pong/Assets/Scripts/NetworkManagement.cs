using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManagement : MonoBehaviour
{
    private SocketIOComponent socket;
    public GameObject go;


    void Start()
    {
        socket = go.GetComponent<SocketIOComponent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
