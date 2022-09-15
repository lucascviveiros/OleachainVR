using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeInteractableInformation : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    public void Enable()
    {
        //enabled = true;
        canvas.SetActive(true);
    }

    public void Reset()
    {
        //enabled = false;
        canvas.SetActive(false);
    }

}
