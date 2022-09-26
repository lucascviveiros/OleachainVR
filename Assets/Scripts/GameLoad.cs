using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    private bool _loading = false;

    public void Start()
    {
        //LoadScene(1);
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
}
