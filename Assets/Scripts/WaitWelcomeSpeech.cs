using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitWelcomeSpeech : MonoBehaviour
{
    [SerializeField] private GameLoad gameLoad;

    private void Awake() 
    {
        gameLoad = FindObjectOfType<GameLoad>();
    }
    
    private void LoadSceneAfterSpeech()
    {
        gameLoad.LoadScene(2);
    }
    
    void OnEnable() 
    {
        TypeWriterEffect.OnWelcomeSpeechFinished += LoadSceneAfterSpeech;
    }

    void OnDisable() 
    {
        TypeWriterEffect.OnWelcomeSpeechFinished -= LoadSceneAfterSpeech;
    }
}
