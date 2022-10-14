using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingText : MonoBehaviour
{
    private Text rankingText;

    void Awake()
    {
        rankingText = GetComponentInChildren<Text>();
    }

    public void SetToggleText(string name, string score)
    {
        rankingText.text = name + " | score: " + "<b><color=#ffff00ff>"+score+"</color></b>";
    }
}
