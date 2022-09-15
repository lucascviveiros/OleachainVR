using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    private GameObject deliverObj;
    public int secondsLeft = 30;
    public bool takingAwak;
    [SerializeField] private Transform referenceTransform2;
    [SerializeField] private Image counterColor;
    [SerializeField] private AudioSource audioFinishing;
    [SerializeField] private AddScore addScore;

    void Start()
    {
        var Obj = Resources.Load("Prefabs/DeliveryHightlight");
        deliverObj = Obj as GameObject;
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        addScore = GetComponent<AddScore>();
        addScore.EnableScoreInTime(true);
    }

    void Update()
    {
        if(takingAwak == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTaker());
        }
    }

    IEnumerator TimerTaker()
    {
        addScore.EnableScoreInTime(true);

        takingAwak = true;
        yield return new WaitForSecondsRealtime(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        takingAwak = false;

        if (secondsLeft == 1 || secondsLeft == 2 || secondsLeft == 3) 
        {
            counterColor.color = UnityEngine.Color.yellow;
            audioFinishing.Play();
            //Play Audio
        }
        
        if(secondsLeft == 0)
        {
            counterColor.color = UnityEngine.Color.red;
            addScore.EnableScoreInTime(false);
            DeliverFood();
            //Play audio
        }
    }

    public void ReturnTimer()
    {
        counterColor.color = Color.green;
        secondsLeft = 15;
    }

    private void DeliverFood()
    {
        Instantiate(deliverObj, referenceTransform2.transform.position, referenceTransform2.transform.rotation);
        // Play Audio
    }
}
