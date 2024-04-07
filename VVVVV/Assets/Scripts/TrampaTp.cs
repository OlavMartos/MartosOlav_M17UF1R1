using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaTp : MonoBehaviour
{
    public Transform target;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            collider.transform.position = target.position;
        }
    }
}
