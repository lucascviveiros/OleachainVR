using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameLoad gameLoad;
    private bool chonsenLanguage;

    public void ChooseLanguage(string language)
    {
        Debug.Log("Chosen language: " + language);
        if (language == "P")
            chonsenLanguage = false;
        else if(language == "E")
            chonsenLanguage = true;

        CallScene(chonsenLanguage);
    }

    private void CallScene(bool chonsenLanguage)
    {
        if(!chonsenLanguage)
            gameLoad.LoadScene(1); //PT
        else    
            gameLoad.LoadScene(2); //EN
    }

    public bool GetSceneChonsen()
    {
        return chonsenLanguage;
    }
}
