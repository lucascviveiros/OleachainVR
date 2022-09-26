using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryFood : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameLogic gameLogic;
    private bool ChangeFood = false;
    public AudioClip audioClip1;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FoodGame" && !ChangeFood)
        {
            ChangeFood = true;
            StartCoroutine(UpdateFood(other.gameObject));
        }
    }

    private IEnumerator UpdateFood(GameObject other)
    {
        if (ChangeFood)
        {
            Destroy(other.gameObject, 1.0f);
            audioSource.PlayOneShot(audioClip1, 1f);
            gameLogic.SpawnAgain();
        }

        yield return new WaitForSecondsRealtime(1.0f);

        ChangeFood = false;

        Destroy(gameObject, 0.1f);

    }
}
