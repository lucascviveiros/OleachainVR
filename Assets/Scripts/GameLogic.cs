using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private GameObject foodObj;
    private float nextSpawnTime;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private float spawnDelay = 15;
    [SerializeField] private Transform referenceTransform;
    [SerializeField] private TimerCountdown timer;
    [SerializeField] List<GameObject> resourcesList;

    private int foodIndex = 0;

    private void Awake()
    {
        timer = FindObjectOfType<TimerCountdown>();
        
        resourcesList = new List<GameObject>();

        var Obj = Resources.Load("Prefabs/Octopus2");

        resourcesList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Chicken");

        resourcesList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Pizza2");
        
        resourcesList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Cheese");
        
        resourcesList.Add(Obj as GameObject);

        //Obj = Resources.Load("Prefabs/Salmon");
        //resourcesList.Add(Obj as GameObject);
    }

    void Start()
    {
        SpawnAgain();
    }

    public void SpawnAgain()
    {
        timer.ReturnTimer();

        Vector3 particleY = new Vector3(0.0f, 0.5f, 0.0f);
        particleY += referenceTransform.transform.position;
        Instantiate(particleSystem, particleY, referenceTransform.transform.rotation);

        /*
        if (foodIndex == 2)
        {
            GameObject obj = Instantiate(resourcesList[foodIndex], referenceTransform.transform.position, referenceTransform.transform.rotation);
            obj.transform.Rotate(188.0f, 0.0f, 0.0f, Space.Self);
        }

        else
        {*/
            Instantiate(resourcesList[foodIndex], referenceTransform.transform.position, referenceTransform.transform.rotation);
        //}

        //Debug.Log("count list: " + resourcesList.Count + " index: " + foodIndex);

        if (foodIndex < resourcesList.Count - 1)
        {
            foodIndex++;
        }
        else
        {
            foodIndex = 0;
        }

    }
 
}
