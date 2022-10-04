using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    public TextMeshProUGUI t_TimerDisplay;
    [SerializeField] private TimerCountdown timerCountdown;
    [SerializeField] private Transform referenceDeliveryFood;
    [SerializeField] private AddScore addScore;
    private GameObject p_deliveryHighlight;
    private int initialSeconds = 30;

    void Awake()
    {
        referenceDeliveryFood = GameObject.Find("[GAME_LOGIC]/ReferenceForDeliveringFood").GetComponent<Transform>();
        timerCountdown = FindObjectOfType<TimerCountdown>();
        t_TimerDisplay = GameObject.Find("[CANVAS_UI]/GameCanvas/CanvasTimerScore/Panel/TextTimerDisplay").GetComponent<TextMeshProUGUI>();
        t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + initialSeconds;
        var Obj = Resources.Load("Prefabs/DeliveryHightlight");
        p_deliveryHighlight = Obj as GameObject;
    }

    void Start()
    {
        addScore = GetComponent<AddScore>();
        addScore.EnableScoreTimer(true);  
        timerCountdown.SetInitialSecondsLeft(initialSeconds);
    }

    public void FoodDelivered()
    {
        timerCountdown.ReturnTimer();
    }

    private void DeliverFood()
    {
        addScore.EnableScoreTimer(false);
        Instantiate(p_deliveryHighlight, referenceDeliveryFood.transform.position, referenceDeliveryFood.transform.rotation);
    }

    private void ChangeTimerDisplay()
    {
        initialSeconds--;
        t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + initialSeconds;
    }

    void OnEnable() 
    {
        TimerCountdown.OnTimerFinished += DeliverFood;
        TimerCountdown.OnTimerChange += ChangeTimerDisplay;
    }

    void OnDisable()
    {
        TimerCountdown.OnTimerFinished -= DeliverFood;
        TimerCountdown.OnTimerChange -= ChangeTimerDisplay;
    }
}
