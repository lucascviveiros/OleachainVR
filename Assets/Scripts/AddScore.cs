using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{    
    public static AddScore Instance {get; private set; }
    public bool activateScore;
    public bool enableScoreTimer;
    public Text oliveOilText;

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (activateScore && enableScoreTimer)
            SumScore.Add(Mathf.RoundToInt(Time.deltaTime * 100));
    }

    public void AddPoints(int points)
    {
        SumScore.Add(points);
    }

    public void EnableScoreInTime(bool enableScore)
    {
        enableScoreTimer = enableScore;
    }

    public void WrongOliveOil()
    {
        oliveOilText.text = "Wrong Oil";
        oliveOilText.color = Color.red;
    }

    public void RightOliveOil()
    {
        oliveOilText.text = "Boa!";
        oliveOilText.color = Color.green;
    }
}
