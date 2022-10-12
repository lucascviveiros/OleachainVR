using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFirebase : MonoBehaviour
{
    private GameObject FirebaseManagerToDetroy;

    void Awake()
    {
        FirebaseManagerToDetroy = GameObject.Find("[FIREBASE_MANAGER]");
        if (FirebaseManagerToDetroy != null)
            Destroy(FirebaseManagerToDetroy);
    }
}
