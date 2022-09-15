using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsCollider : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter: " + other.name);
    }

}
