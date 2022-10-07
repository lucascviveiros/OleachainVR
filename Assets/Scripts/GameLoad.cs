using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    private SceneController sceneController;
    private bool _loading = false;
    private bool language = false;
   
    void Start()
    {
        sceneController = GameObject.FindObjectOfType<SceneController>();
    }

    public void LoadScene(int levelIndex)
    {
        if (_loading) return;
        _loading = true;
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    private IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while(!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }

    void Load(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadQuiz()
    {
        if(sceneController.GetSceneChonsen()) //en
        {
            LoadScene(3);
        }
        else
        {
            LoadScene(4);
        }       
    }
}
