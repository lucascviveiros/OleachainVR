using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoParent : MonoBehaviour
{
    void Start()
    {

        this.transform.SetParent(null);       
    }
}
