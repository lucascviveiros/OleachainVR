using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private StreamLine currentStream = null;

    [SerializeField] private bool activate = true;

    [SerializeField] private AudioSource audioSource;

    //private Animation animation;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (activate)
        {
            bool pourCheck = CalculatePourAngle() < pourThreshold;

            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;

                if (isPouring)
                {
                    StartPour();
                }
                else
                {
                    EndPour();
                }
            }
        }
        else
        {
            // Empty
        } 
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
        PlayLiquidSound();
    }

    public void EndPour()
    {
        currentStream.End();
        currentStream = null;
        StopLiquidSound();
    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private StreamLine CreateStream()
    {
        GameObject streamObj = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObj.GetComponent<StreamLine>();
    }
    private void PlayLiquidSound() 
    {
        audioSource.Play();
    }

    public void StopLiquidSound()
    {
        audioSource.Stop();
    }

    public bool IsPouring()
    {
        return isPouring;
    }

}