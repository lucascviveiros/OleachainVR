using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjustable : MonoBehaviour
{
    [SerializeField] private  GameObject CenterEyeAnchor;
    [SerializeField] private GameObject moveObjWithCamPosition;
    [SerializeField] private string findObjectName = "VirtualKeyboard";
    [SerializeField] private float z_axisDist = 0.9f;
    [SerializeField] private float x_axisDist = 0.09f;
    [SerializeField] private float y_axisMaxDist = 0.9f;
    [SerializeField] private float y_axisMinDist = 0.6f;
    private float SPEED = 0.5f;

    private void Awake() 
    {
        moveObjWithCamPosition = GameObject.Find(findObjectName);
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
    }

    private void Update()
    {
        HeadLock();
    }

    private void HeadLock()
    {
        float speed;
        speed = Time.deltaTime * SPEED;

        Vector3 posTo = CenterEyeAnchor.transform.position + (CenterEyeAnchor.transform.forward * z_axisDist);
        posTo.y = posTo.y - 0.5f;

        if (posTo.y >= y_axisMaxDist)
        {
            posTo.y = y_axisMaxDist;
        }
        else if (posTo.y <= y_axisMinDist)
        {
            posTo.y = y_axisMinDist;
        }

        posTo.x = x_axisDist;
        moveObjWithCamPosition.transform.position = Vector3.SlerpUnclamped(moveObjWithCamPosition.transform.position, posTo, speed);    
    }
}
