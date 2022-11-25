using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjustable : MonoBehaviour
{
    [SerializeField] private  GameObject CenterEyeAnchor;
    [SerializeField] private GameObject objectToMoveWithCameraPosition;
    [SerializeField] private string findObjectName = "VirtualKeyboard";
    [SerializeField] private float DISTANCE = 0.9f;
    [SerializeField] private float xDistance = 0.09f;
    [SerializeField] private float yMaxDistanceLimit = 0.9f;
    [SerializeField] private float yMinDistanceLimit = 0.6f;
    private float SPEED = 0.5f;

    private void Awake() 
    {
        objectToMoveWithCameraPosition = GameObject.Find(findObjectName);
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
    }

    private void HeadLock()
    {
        float speed;
        speed = Time.deltaTime * SPEED;

        Vector3 posTo = CenterEyeAnchor.transform.position + (CenterEyeAnchor.transform.forward * DISTANCE);
        posTo.y = posTo.y - 0.5f;

        if (posTo.y >= yMaxDistanceLimit)
        {
            posTo.y = yMaxDistanceLimit;
        }
        else if (posTo.y <= yMinDistanceLimit)
        {
            posTo.y = yMinDistanceLimit;
        }

        posTo.x = xDistance;
        objectToMoveWithCameraPosition.transform.position = Vector3.SlerpUnclamped(objectToMoveWithCameraPosition.transform.position, posTo, speed);    
               
    }
 
    private void Update()
    {
        HeadLock();
    }
}
