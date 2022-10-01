using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject popUpPrefab;
    public Sprite icon;

    public void ShowPopUp()
    {
        var obj = Instantiate(popUpPrefab,FindObjectOfType<Canvas>().transform);
        obj.GetComponent<PopUpController>().ShowPopUp(icon);
    }
}
