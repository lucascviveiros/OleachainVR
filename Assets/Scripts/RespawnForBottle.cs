using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnForBottle : MonoBehaviour
{
    Vector3 initialWorldPos;
    Quaternion initialWorldRot;

    [Header("Control")]
    public float respawnUnderHeigth = 0.1f;

    public PourDetector pourDetector;

    [SerializeField]
    private Rigidbody _rigidBody;

    [SerializeField]
    private GameObject streamingLine;

    private bool once;

    [Header("Control")]
    public float rapawnWhenHigher = 0.3f;

    //[SerializeField] private GameObject SafeArea;

    private void Start()
    {
      //  SafeArea = GameObject.FindGameObjectWithTag("SafeArea");
        _rigidBody = GetComponent<Rigidbody>();
        pourDetector = GetComponent<PourDetector>();
        initialWorldPos = transform.position;
        initialWorldRot = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y <= respawnUnderHeigth && !once)
        {
            RespawnObject();
        }

        if (transform.position.y >= rapawnWhenHigher && !once)
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
         } 

         */

    }

    private void RespawnObject()
    {
        once = true;
        if (pourDetector.IsPouring())
        {
            streamingLine = GetComponentInChildren<StreamLine>().gameObject;
        }

        if (pourDetector && streamingLine)
        {
            //pourDetector.EndPour();
            pourDetector.StopLiquidSound();
        }

        StartCoroutine(WaitFor());

        if (_rigidBody)
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
        }
        //transform.position = initialWorldPos;
        //transform.rotation = initialWorldRot;
    }

    private IEnumerator WaitFor()
    {
        yield return new WaitForSecondsRealtime(2f);
        transform.position = initialWorldPos;
        transform.rotation = initialWorldRot;
        once = false;
    }


}
