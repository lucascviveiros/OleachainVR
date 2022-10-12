using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStartSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSecondsRealtime(5f);
        audioSource.Play();
    }
}
