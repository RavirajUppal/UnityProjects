using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componenetsToDisable;

    [SerializeField]
    string RemoteLayerName = "RemoteLayer";
    string LocalLayerName = "LocalLayer";

    Camera cam;
    enum playerLayers { LocalPlayer = 8, RemotePlayer = 9 }

    void Start()
    {
        
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            cam = Camera.main;
            if (cam != null)
            {
                cam.gameObject.SetActive(false);
            }
        }
        GetComponent<Player>().setup();

   
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }


    void AssignRemoteLayer()
    {
        //gameObject.layer = Layer.NameToLayer(RemoteLayerName);
        gameObject.layer = (int)playerLayers.RemotePlayer;
    }


    void DisableComponents()
    {
        for (int i = 0; i < componenetsToDisable.Length; i++)
        {
            componenetsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        if (cam != null)
        {
            cam.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }
}


