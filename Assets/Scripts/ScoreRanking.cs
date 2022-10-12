using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OleaChainVR
{
    public class ScoreRanking : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private FirebaseManager firebaseManager;
        string savedUserName;

        private void Awake()
        {
            //savedUserName = "fulano5"; //Debugging
            userName = GameObject.Find("CanvasName/Panel/InputName").GetComponent<TextMeshProUGUI>();
            firebaseManager = FindObjectOfType<FirebaseManager>();
        }

        public void SaveUserName()
        {
            savedUserName = userName.text.ToString();
            //SaveUser.UserName = savedUserName;
            PlayerPrefs.SetString("USER_NAME", savedUserName);
        }

        public void SaveScore(int score)
        {
            Debug.Log("Score Saved: " + score);
        }

        
    }
}
