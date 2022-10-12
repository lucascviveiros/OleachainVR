using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechGame : MonoBehaviour
{
    [SerializeField] private SpeechManager speechManager;

    void Start()
    {  
        speechManager = FindObjectOfType<SpeechManager>();      
        speechManager.TutorialPT_EN();
    }

}
