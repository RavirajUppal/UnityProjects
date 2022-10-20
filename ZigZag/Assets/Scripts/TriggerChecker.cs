using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Invoke ("fallDown" ,0.1f);
 
        }
        if(col.gameObject.tag == "diamond")
        {
            col.gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(col.gameObject, 2f);
        }
    }
  
    void fallDown()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 2f);
    }
  
}
