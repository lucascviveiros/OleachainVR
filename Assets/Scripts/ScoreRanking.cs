using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

///Start on TypeName scene
public class ScoreRanking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;

    void Start()
    {
        userName = GameObject.Find("CanvasName/Panel/InputName").GetComponent<TextMeshProUGUI>();
    }

    public void SaveUserName()
    {
        string saveUserName = userName.text.ToString();
        Debug.Log("UserName saved: " + saveUserName);
    }

    public void SaveScore(int score)
    {
        Debug.Log("Score Saved: " + score);
    }
}
