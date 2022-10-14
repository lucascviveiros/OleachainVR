using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace OleaChainVR
{
    public class ShowScore : MonoBehaviour
    {
        private FirebaseManager firebaseManager;
        private List<UserScore> rankingList;
        private List<UserScore> orderedRankingList;
        private GameObject scoreItem;
        [SerializeField] private GameObject invisibleReference;
        [SerializeField] private GameObject CanvasScore;
        private string RankingName; 
        private string RankingScore;

        void Awake()
        {
            var Obj = Resources.Load("Prefabs/UI/ToggleScore");
            scoreItem = (GameObject)Obj;

            CanvasScore = GameObject.Find("CanvasScore");
            //CanvasScore.SetActive(false);

            rankingList = new List<UserScore>();
            firebaseManager = FindObjectOfType<FirebaseManager>();

            if (firebaseManager != null)
                StartCoroutine(GetScoresFromFirebase());
        }

        private IEnumerator GetScoresFromFirebase()
        {
            yield return new WaitForSecondsRealtime(3f);
            yield return firebaseManager.GetScore();
        }

        public void UpdateRanking(List<UserScore> rankingList)
        {
            orderedRankingList = rankingList.OrderByDescending(x => x.Score).ToList();

            for(int i = 0; i < orderedRankingList.Count; i++)
            {
                GameObject rankingItem = Instantiate(scoreItem);
                rankingItem.name = "rankingItem_" + i;
                rankingItem.transform.SetParent(invisibleReference.transform.parent, false);
                RankingName = orderedRankingList.ElementAt(i).Name.ToString();
                RankingScore = orderedRankingList.ElementAt(i).Score.ToString();
                Debug.Log("Name: " + "["+RankingName+"]" + " Score: " + "["+RankingScore+"]");
                rankingItem.GetComponent<RankingText>().SetToggleText(RankingName, RankingScore); 
            }

            //CanvasScore.SetActive(true);
        }
    }
}
