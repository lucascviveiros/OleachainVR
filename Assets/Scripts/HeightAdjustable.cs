using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjustable : MonoBehaviour
{
    [SerializeField] private  GameObject CenterEyeAnchor;
    [SerializeField] private GameObject virtualKeyboard;
    private const float DISTANCE = 0.9f;
    private float SPEED = 0.5f;
    public float xDistance = 0.09f;
    
    private void Awake() 
    {
        virtualKeyboard = GameObject.Find("VirtualKeyboard");
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
    }

    private void HeadLock()
    {
        float speed;
        speed = Time.deltaTime * SPEED;

        Vector3 posTo = CenterEyeAnchor.transform.position + (CenterEyeAnchor.transform.forward * DISTANCE);
        posTo.y = posTo.y - 0.5f;

        if (posTo.y >= 0.9f)
        {
            posTo.y = 0.9f;
        }
        else if (posTo.y <= 0.6f)
        {
            posTo.y = 0.6f;
        }

        posTo.x = xDistance;
        virtualKeyboard.transform.position = Vector3.SlerpUnclamped(virtualKeyboard.transform.position, posTo, speed);    
               
    }
 
    private void Update()
    {
        HeadLock();
    }
}
