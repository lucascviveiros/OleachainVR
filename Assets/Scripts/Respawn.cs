using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 initialWorldPos;
    Quaternion initialWorldRot;

    [Header("Control")]
    public float respawnUnderHeigth = 0.1f;

    [SerializeField]
    private Rigidbody _rigidBody;

    private bool _safeArea = false;

//    [SerializeField] private GameObject SafeArea;


    private void Start()
    {
        //SafeArea = GameObject.FindGameObjectWithTag("SafeArea");
        _rigidBody = GetComponent<Rigidbody>();
        initialWorldPos = transform.position;
        initialWorldRot = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y <= respawnUnderHeigth)
        {
            RespawnObject();  
        }

        else if (_safeArea)
        {
            RespawnObject();
        }

        /*
        if (transform.localPosition.x > SafeArea.transform.localPosition.x)
        {
            Debug.Log("Outside safe area X");
            RespawnObject();
        }
        else if (transform.localPosition.y > SafeArea.transform.localPosition.y)
        {
            Debug.Log("Outside safe area Y");

            RespawnObject();
        }
        else if (transform.localPosition.z > SafeArea.transform.localPosition.z)
        {
            Debug.Log("Outside safe area Z");

            RespawnObject();
        }         */
    }

    private void RespawnObject()
    {
        if (_rigidBody)
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }

        transform.position = initialWorldPos;
        transform.rotation = initialWorldRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SafeArea")
        {
            _safeArea = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if(other.gameObject.tag == "SafeArea")
        {
            _safeArea = true ;
        }
    }
}
