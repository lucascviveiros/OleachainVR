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
        private string savedUserName;

        private void Awake()
        {
            userName = GameObject.Find("CanvasName/Panel/InputName").GetComponent<TextMeshProUGUI>();
            firebaseManager = FindObjectOfType<FirebaseManager>();
        }

        public void SaveUserName()
        {
            savedUserName = userName.text.ToString();
            PlayerPrefs.SetString("USER_NAME", savedUserName);
        }

        public void SaveScore(int score)
        {
            Debug.Log("Score Saved: " + score);
        }
    }
}
