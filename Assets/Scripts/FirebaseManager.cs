using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

namespace OleaChainVR
{
    public class FirebaseManager : MonoBehaviour
    {
        public class UserScore
        {
            public string name;
            public int score;

            public string Name
            {
                get {return name; }
                set { name = value; }
            }

            public int Score
            {
                get {return score; }
                set { score = value; }
            }

            public UserScore(string name, int score)
            {
                this.name = name;
                this.score = score;
            }
        }
        private string ID_time;
        private DatabaseReference databaseReference;
    
        public static FirebaseManager Instance; 

        private void Awake() 
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }    
        }

        void Start()
        {
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public void CreateUser(string name, int final_score)
        {
            ID_time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
            FirebaseManager.UserScore newUser = new FirebaseManager.UserScore(name, final_score);
            string json = JsonUtility.ToJson(newUser);
            databaseReference.Child("users").Child(ID_time).SetRawJsonValueAsync(json);
        }

        public IEnumerator GetScore()        //Action<string> onCallback)
        {
            var userNameScoreTask = databaseReference.Child("users").OrderByKey().GetValueAsync();

            yield return new WaitUntil(predicate: () => userNameScoreTask.IsCompleted);

            if(userNameScoreTask == null)
            {
                Debug.LogWarning(message: $"Failed to register task with {userNameScoreTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = userNameScoreTask.Result;

                foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
                {
                    string username = childSnapshot.Child("name").Value.ToString();
                    int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                }
            }
        }

        public void ReceiveFinalQuizData(string quiz_name, int quiz_score)
        {
            CreateUser(quiz_name, quiz_score);
        }
    }
}
