using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    private int secondsLeft = 0; 
    private bool b_secondsControl;
    [SerializeField] private Image counterColor;
    [SerializeField] private AudioSource audioFinishing;
  
    //Events
    public delegate void TimerFinished();
    public static event TimerFinished OnTimerFinished;
    public delegate void TimerChange();
    public static event TimerChange OnTimerChange;

    public void SetInitialSecondsLeft(int initialSecondsLeft)
    {
        secondsLeft = initialSecondsLeft;
    }

    void Update()
    {
        if(b_secondsControl == false && secondsLeft > 0)
        {
            StartCoroutine(TimerCounter());
        }
    }

    private IEnumerator TimerCounter()
    {
        b_secondsControl = true;
        yield return new WaitForSecondsRealtime(1);
        secondsLeft -= 1;
        OnTimerChange(); //Enable Event
        b_secondsControl = false;

        if (secondsLeft == 1 || secondsLeft == 2 || secondsLeft == 3) 
        {
            counterColor.color = UnityEngine.Color.yellow;
            audioFinishing.Play();
        }
        
        if(secondsLeft == 0)
        {
            counterColor.color = UnityEngine.Color.red;
            OnTimerFinished(); //Enable event        
        }
    }

    public void ReturnTimer()
    {
        counterColor.color = Color.green;
        secondsLeft = 15;
    }
}
