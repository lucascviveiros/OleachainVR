using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameLoad gameLoad;

    private bool b_testOnce = false;

    private void Awake() 
    {
        gameLoad = GameObject.Find("[GAME_LOAD]").GetComponent<GameLoad>();
        
        //if (!b_testOnce) //for debugging
            //ChooseLanguage("P");
            //b_testOnce = true;

    }

    public void ChooseLanguage(string language)
    {
        if (language == "P")
            PlayerPrefs.SetInt("LANGUAGE", 0);
        
        if(language == "E")
            PlayerPrefs.SetInt("LANGUAGE", 1);

            //b_language = true;

        CallScene();
    }

    private void CallScene()
    {
        gameLoad.LoadScene(1); 
    }
    /*
    public bool GetSceneChonsen()
    {
        return b_language;
    }*/

    public string GetCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        return currentScene;
    }

    public void StartQuiz()
    {
        //if(GetSceneChonsen()) //en
        if(PlayerPrefs.GetInt("LANGUAGE") == 1)
        {
            gameLoad.LoadScene(3);
        }
        else
        {
            gameLoad.LoadScene(4);
        }     
    }

    public void StartLanguage()
    {
        gameLoad.Load(0);
    }
}
