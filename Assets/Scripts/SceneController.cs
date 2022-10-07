using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameLoad gameLoad;
    private bool b_language;
    private bool b_testOnce = false;

    private void Start() 
    {
        //if (!b_testOnce) //for debugging
            //ChooseLanguage("P");
            //b_testOnce = true;

        //Debug.Log("Scene: " + GetCurrentScene());
    }

    public void ChooseLanguage(string language)
    {
        if (language == "P")
            b_language = false;
        else if(language == "E")
            b_language = true;

        CallScene();
    }

    private void CallScene()
    {
        gameLoad.LoadScene(1); 
    }

    public bool GetSceneChonsen()
    {
        return b_language;
    }

    public string GetCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        return currentScene;
    }
}
