using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //private float nextSpawnTime;
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private Transform referenceToInstantiate;
    [SerializeField] private DeliveryManager deliveryManager; //TimerCountdown timer;
    [SerializeField] private List<GameObject> resourceFoodList;
    private GameObject foodObj;
    private int foodIndex = 0;

    private void Awake()
    {
        referenceToInstantiate = GameObject.Find("[GAME_LOGIC]/ReferenceToInstantiateFood").GetComponent<Transform>();
        
        deliveryManager = FindObjectOfType<DeliveryManager>();
        //timer = FindObjectOfType<TimerCountdown>();
        
        resourceFoodList = new List<GameObject>();

        var particleSystem = Resources.Load("Prefabs/EnergyExplosionLight");
        explosionParticle = particleSystem as GameObject;

        var Obj = Resources.Load("Prefabs/Octopus2");

        resourceFoodList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Chicken");

        resourceFoodList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Pizza2");
        
        resourceFoodList.Add(Obj as GameObject);

        Obj = Resources.Load("Prefabs/Cheese");
        
        resourceFoodList.Add(Obj as GameObject);

    }

    void Start()
    {
        SpawnAgain();
    }

    public void SpawnAgain()
    {
        deliveryManager.FoodDelivered();
        //timer.ReturnTimer();

        Vector3 particleY = new Vector3(0.0f, 0.5f, 0.0f);
        particleY += referenceToInstantiate.transform.position;
        Instantiate(explosionParticle, particleY, referenceToInstantiate.transform.rotation);

        //GameObject obj = Instantiate(resourcesList[foodIndex], referenceTransform.transform.position, referenceTransform.transform.rotation);
        //obj.transform.Rotate(188.0f, 0.0f, 0.0f, Space.Self);

        Instantiate(resourceFoodList[foodIndex], referenceToInstantiate.transform.position, referenceToInstantiate.transform.rotation);
        
        //Debug.Log("count list: " + resourcesList.Count + " index: " + foodIndex);

        if (foodIndex < resourceFoodList.Count - 1)
        {
            foodIndex++;
        }
        else
        {
            //Finishing meals / finishing game
            foodIndex = 0;
        }

    }
 
}
