using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Random = System.Random;
using System;

namespace OleaChainVR
{
    public class QuizManager : MonoBehaviour
    {
        public class Quiz
        {
            private string answer;
            private string question;
            private string options;
            private string answerDescription;

            public string Question
            {
                get {return question; }
                set { question = value; }
            }

            public string Options
            {
                get {return options; }
                set { options = value; }
            }
            
            public string Answer
            {
                get {return answer; }
                set { answer = value; }
            }

            public string AnswerDescription
            {
                get {return answerDescription; }
                set { answerDescription = value; }
            }

            public Quiz(string question, string options, string answer, string answerDescription)
            {
                this.question = question;
                this.options = options;
                this.answer = answer;
                this.answerDescription = answerDescription;
            }
        }
        
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject particleSystem;
        [SerializeField] private Transform referenceTransform;
        [SerializeField] private GameObject virtualKeyboard;
        [SerializeField] private FirebaseManager firebaseManager;
        [SerializeField] private ScoreRanking scoreRanking;
        private TextMeshProUGUI t_question;
        private TextMeshProUGUI t_options;
        private TextMeshProUGUI t_answerDescription;
        private TextMeshProUGUI t_score;
        private GameObject CanvasPrincipal;
        private GameObject CanvasAnswer;
        private GameObject CanvasScore;
        private TextMeshProUGUI t_TimerDisplay;
        private TimerCountdown timerCountdown;
        private AudioClip clip;
        private bool lockAnswer;
        private int round = 0;
        private int initialSeconds = 10;
        private List<QuizManager.Quiz> randomQuiz = new List<QuizManager.Quiz>();
        private SceneController sceneController;
        private int scoreQuiz = 0; 
        public static QuizManager Instance { get; private set; }

        private List<string> question = new List<string>
        {
            "Qual dos seguintes instrumentos é utilizado na colheita tradicional?", //1
            "Estima-se que a oliveira mais antiga do mundo tenha ...", //2
            "As “alcaparras” também conhecidas por azeitonas verdes descaroçadas são típicas de que região de Portugal?", //3
            "Como se avalia a qualidade de um azeite?", //4
            "Qual o maior consumidor de azeite no mundo?", //5
            "O olival transmontano é predominantemente:", //6
            "Quantas pessoas, em média, são precisas para abraçar a oliveira mais antiga de Portugal?", //7
            "Onde se situa a oliveira mais antiga de Portugal?", //8
            "Os ramos da oliveira são associados a:", //9
            "Um Azeite Virgem Extra é?", //10
            "Quais são as variedades de oliveiras predominantes na região transmontana?", //11
            "Em que meses do ano ocorre, normalmente, a colheita da azeitona em Portugal?", //12
            "Quantas calorias têm, aproximadamente, 100 gramas de azeite?", //13
            "Qual é o azeite de menor qualidade?", //14
            "As folhas de oliveira apresentam características:", //15
            "As  oliveiras são resistentes?", //16
            "Qual o período mais indicado para a prática da plantação das oliveiras?", //17
            "Quanto tempo demora até as oliveiras dar frutos ?", //18
            "As folhas de oliveira podem ser usadas em?", //19
            "Como se chama o local onde é extraído o azeite?" //20
        };

        private List<string> question_en = new List<string>
        {
            "Which of the following instruments is used in traditional harvesting?", //1
            "The oldest olive tree in the world is estimated to have...", //2
            "The capers also known as pitted green olives are typical of which region of Portugal?", //3
            "How is the quality of an olive oil evaluated?", //4
            "Who is the biggest consumer of olive oil in the world?", //5
            "The Trás-os-Montes olive grove is predominantly", //6
            "How many people, on average, does it take to embrace the oldest olive tree in Portugal?", //7
            "Where is the oldest olive tree in Portugal?", //8
            "The branches of the olive tree are associated with:", //9
            "An Extra Virgin Olive Oil is?", //10
            "What are the predominant varieties of olive trees in the Trás-os-Montes region?", //11
            "In which months of the year does the olive harvest normally take place in Portugal?", //12
            "How many calories are in approximately 100 grams of olive oil?", //13
            "What is the lowest quality olive oil?", //14
            "Olive leaves have characteristics:",//15
            "Are olive trees hardy?", //16
            "What is the most suitable period for the practice of planting olive trees?", //17
            "How long does it take for olive trees to bear fruit?", //18
            "Can olive leaves be used in?", //19
            "What is the name of the place where the oil is extracted?" //20
        };

        private List<string> options = new List<string>
        {
            "A. Vara.\nB. Sachola.\nC. Trator.\nD. Foice.", //1
            "A. ... de 3 a 5 mil anos.\nB. ... menos de 500 anos.\nC. ... de 6 a 9 mil anos.\nD. ... de 500 anos a 2000 anos.", //2
            "A. Estremadura.\nB. Trás-os-Montes.\nC. Alentejo.\nD. Minho", //3
            "A. Pela leitura do rótulo.\nB. Através da avaliação dos parâmetros de qualidade.\nC. Através da acidez.D.\nAtravés da cor do azeite", //4
            "A. Portugal.\nB. Grécia.\nC. Itália.\nD. Espanha", //5
            "A. Tradicional\nB. Intensivo.\nC. Super intensivo.\nD. Não existe olival em Trás-os-Montes.", //6        
            "A. 5 pessoas.\nB. 33 pessoas.\nC. 12 pessoas.\nD. 7 pessoas.", //7
            "A. Gostei – Bragança.\nB. Cascalhos – Abrantes.\nC. Bemposta – Mogadouro.\nD. Arraiolos – Évora.", //8
            "A. Morte.\nB. Paz.\nC. Fertilidade.\nD. Pureza.", //9
            "A. Um azeite feito com azeitonas uma única cultivar.\nB. Uma gordura extraída apenas por processos físicos e mecânicos.\nC. Um azeite de qualidade superior.\nD. Um azeite que não tem qualquer qualidade.", //10
            "A. Cordovil, Carrasquenha, Picual, Negrinha e Gordal.\nB. Picual, Arbequina, Leccino, Coratina e Frantoio.\nC. Verdeal, Madural, Negrinha, Santulhana e Cobrançosa.\nD. Coratina, Missão, Kalamata, Gordal e Verdeal.", //11
            "A. De Janeiro a Março.\nB. De Março a Maio.\nC. De Junho a Agosto.\nD. De Outubro a Janeiro.", //12
            "A. 900 kcal.\nB. 100 kcal.\nC. 650 kcal.\nD.220 kcal.", //13
            "A. Lampante.\nB. Virgem.\nC. Virgem extra.\nD. Puro.", //14
            "A. Caducas.\nB. Caducifólia.\nC. Perenes.\nD. Compostas.", //15
            "A. À poda.\nB. Temperaturas extremas, geadas e sol excessivo.\nC Colheita mecânica.\nD. Não são resistentes.", //16
            "A. Setembro e outubro e/ou março e abril.\nB. Janeiro e fevereiro e/ou novembro e dezembro.\nC. Setembro e Outubro e/ou Janeiro e Fevereiro.\nD. Março e Abril e/ou Julho e Agosto.", //17
            "A. No primeiro ano de plantação.\nB. A maioria de três a cinco anos de idade.\nC. Apenas após 10 anos de idade\nD. Produzem em qualquer ano de idade.", //18
            "A. Preparação de infusões\nB. Preparação de azeitonas de mesa.\nC. Extração de azeite.\nD. Preparação de farinhas.", //19
            "A. Queijaria.\nB. Talho.\nC. Lagar.\nD. Adega." //20
        };

        private List<string> options_en = new List<string>
        {
            "A. Rod.\nB. Hoe.\nC. Tractor.\nD. Sickle.", //A
            "A. ... from 3 to 5 thousand years.\nB. ... less than 500 years.\nC. ... from 6 to 9 thousand years.\nD. ... from 500 years to 2000 years. ", //A
            "A. Estremadura.\nB. Trás-os-Montes.\nC. Alentejo.\nD. Minho",
            "A. By reading the label.\nB. Through the evaluation of quality parameters.\nC. Through acidity.\nD. Through the color of the oil.",
            "A. Portugal.\nB. Greece.\nC. Italy.\nD. Spain", //5
            "A. Traditional\nB. Intensive.\nC. Super intensive.\nD. There is no olive grove in Trás-os-Montes.", //6
            "A. 5 people.\nB. 33 people.\nC. 12 people.\nD. 7 people.", //7
            "A. Gostei – Bragança.\nB. Cascalhos – Abrantes.\nC. Bemposta – Mogadouro.\nD. Arraiolos – Évora.", //8
            "A. Death.\nB. Peace.\nC. Fertility.\nD. Purity.", //9
            "A. An oil made with olives of a single cultivar.\nB. A fat extracted only by physical and mechanical processes.\nC. A superior quality oil.\nD. An oil that has no quality whatsoever.", //10
            "A. Cordovil, Carrasquenha, Picual, Negrinha and Gordal.\nB. Picual, Arbequina, Leccino, Coratina and Frantoio.\nC. Verdeal, Madural, Negrinha, Santulhana and Cobrançosa.\nD. Coratina, Missão, Kalamata, Gordal and Verdeal.", //11
            "A. From January to March.\nB. From March to May.\nC. From June to August.\nD. From October to January.", //12
            "A. 900 kcal.\nB. 100 kcal.\nC. 650 kcal.\nD.220 kcal.", //13
            "A. Lampante.\nB. Virgin.\nC. Extra Virgin.\nD. Pure.", //14
            "A. Expiring.\nB. Deciduous.\nC. Perennial.\nD. Composites.", //15
            "A. Pruning.\nB. Extreme temperatures, frost and excessive sun.\n.C Mechanical harvesting.\nD. Not resistant.", //16
            "A. September and October and/or March and April.\nB. January and February and/or November and December.\nC. September and October and/or January and February.\nD. March and April and/or July and August.", //17
            "A. In the first year of planting.\nB. Most are three to five years old.\nC. Only after 10 years old\nD. They breed in any year of age.", //18
            "A. Preparation of infusions\nB. Preparation of table olives.\nC. Olive oil extraction.\nD. Preparation of flours.", //19
            "A. Cheese shop.\nB. Butcher.\nC. Lagar.\nD. Cellar." //20
        };
    
        private string[] answers = new string[20]
        {
            "A", //1 
            "A", //2
            "B", //3
            "B", //4
            "B", //5
            "A", //6 
            "A", //7
            "B", //8
            "B", //9
            "C", //10
            "C", //11
            "D", //12
            "A", //13
            "A", //14
            "C", //15
            "B", //16
            "A", //17
            "B", //18
            "A", //19
            "C" //20
        };

        private string[] answersDescription = new string[20]
        {
            "A - Vara.", //1 
            "A - De 3 a 5 mil anos.", //2
            "B - Trá-os-Montes.", //3
            "B - Através da avaliação dos parâmetros de qualidade.", //4
            "B - Grécia.", //5
            "A - Tradicional.", //6
            "A - 5 pessoas.", //7
            "B - Cascalhos – Abrantes.", //8
            "B - Paz.", //9
            "C - um azeite de qualidade superior.", //10
            "C - Verdeal, Madural, Negrinha, Santulhana e Cobrançosa." , //11       
            "D - De Outubro a Janeiro.", //12
            "A - 900 kcal.", //13
            "A - Lampante.", //14
            "C - Perenes", //15
            "B - Temperaturas extremas, geadas e sol excessivo.", //16
            "A - Setembro e Outubro e/ou Março e Abril.", //17
            "B - A maioria de três a cinco anos de idade.", //18
            "A - Preparação de infusões.", // 19
            "C - Largar." //20
        };

        private string[] answersDescription_en = new string[20]
        {
            "A - Rod.", //1
            "A - From 3 to 5 thousand years.", //2
            "B - Trás-os-Montes.", //3
            "B - Through the evaluation of quality parameters.", //4
            "B - Greece.", //5
            "A - Traditional.", //6
            "A - 5 people.",  //7
            "B - Cascalhos – Abrantes.", //8
            "B - Peace.", //9
            "C - A superior quality oil.", //10
            "C - Verdeal, Madural, Negrinha, Santulhana and Cobrançosa.", //11
            "D - From October to January.", //12
            "A - 900 kcal.", //13
            "A - Lampante.",  //14
            "C - Perennials.", //15
            "B. Extreme temperatures, frost and excessive sun.", //16
            "A - September and October and/or March and April.", //17
            "B - Most are three to five years old.", //18
            "A - Preparation of infusions.", //19
            "C - Largar." //20
        };

        private void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        private void Start() 
        {
            virtualKeyboard = GameObject.Find("VirtualKeyboard");
            t_question = GameObject.Find("[CANVAS_QUIZ]/Panel/TextQuestion").GetComponent<TextMeshProUGUI>();
            t_options = GameObject.Find("[CANVAS_QUIZ]/Panel/TextOptions").GetComponent<TextMeshProUGUI>();
            t_answerDescription = GameObject.Find("[CANVAS_ANSWER]/Panel/TextAnswer").GetComponent<TextMeshProUGUI>();
            t_score = GameObject.Find("[CANVAS_SCORE]/Panel/TextScore").GetComponent<TextMeshProUGUI>();
            t_score.text = "0";
            t_TimerDisplay = GameObject.Find("[CANVAS_QUIZ]/Panel/BackgroungTimerPanel/TextTimerDisplay").GetComponent<TextMeshProUGUI>();
            t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + initialSeconds;
            timerCountdown = FindObjectOfType<TimerCountdown>();
            timerCountdown.SetInitialSecondsLeft(initialSeconds);            
            firebaseManager = FindObjectOfType<FirebaseManager>();

            CanvasAnswer = GameObject.Find("[CANVAS_ANSWER]");
            CanvasScore = GameObject.Find("[CANVAS_SCORE]");
            CanvasPrincipal = GameObject.Find("[CANVAS_QUIZ]");
            CanvasAnswer.SetActive(false);
            CanvasScore.SetActive(false);

            List<QuizManager.Quiz> listQuiz = new List<QuizManager.Quiz>();
            sceneController = GameObject.FindObjectOfType<SceneController>();

            if(PlayerPrefs.GetInt("LANGUAGE") == 1)
            {
                for(int i = 0; i<= (question_en.Count - 1); i++)    
                {
                    listQuiz.Add(new QuizManager.Quiz(question_en.ElementAt(i), options_en.ElementAt(i), answers[i], answersDescription_en[i]));
                }
            }
            else
            {
                for(int i = 0; i<= (question.Count - 1); i++)
                {
                    listQuiz.Add(new QuizManager.Quiz(question.ElementAt(i), options.ElementAt(i), answers[i], answersDescription[i]));
                }
            }        
            
            Random rng = new Random();
            randomQuiz = listQuiz.OrderBy(_ => rng.Next()).ToList();
            t_question.text = randomQuiz.ElementAt(0).Question;
            t_options.text = randomQuiz.ElementAt(0).Options;

            //FinishQuiz(); //Debugging
        }

        private void PlayForCorrectAnswer()
        {
            ActivateParticle();
            clip = audioClips[1];
            audioSource.clip = clip;
            audioSource.Play();
        }
        
        private void PlayForWrongAnswer()
        {
            clip = audioClips[0];
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void ReceiveAnswer(string answer)
        {
            VerifyAnswer(answer);
        }

        private void VerifyAnswer(string option)
        {
            if(!lockAnswer)
            {
                if(randomQuiz.ElementAt(round).Answer == option)
                {
                    AddScore();
                    PlayForCorrectAnswer();
                    ShowScore();
                }
                else
                {
                    ShowRightAnswer(round);
                    PlayForWrongAnswer();
                }

                lockAnswer = true;
                StartCoroutine(WaitForNextAnswer());
            }
        }

        private void ShowRightAnswer(int round)
        {
            CanvasAnswer.SetActive(true);
            t_answerDescription.text = randomQuiz.ElementAt(round).AnswerDescription;
        }

        private void ShowScore()
        {
            CanvasScore.SetActive(true);
            t_score.text = scoreQuiz.ToString();
        }

        private IEnumerator WaitForNextAnswer()
        {
            yield return new WaitForSecondsRealtime(5f);
            t_answerDescription.text = "";
            CanvasAnswer.SetActive(false);
            CanvasScore.SetActive(false);
            lockAnswer = false;
            NextRound();
        }

        private void NextRound()
        {
            if(round < (randomQuiz.Count - 1))
            {
                round++;
                t_question.text = randomQuiz.ElementAt(round).Question;
                t_options.text = randomQuiz.ElementAt(round).Options;
            }
            else
            {
                FinishQuiz(); 
            }
        }

        private void ActivateParticle()
        {
            Vector3 particleY = new Vector3(0.0f, 0.1f, 0.0f);
            particleY += referenceTransform.transform.position;
            var particle = Instantiate(particleSystem, particleY, referenceTransform.transform.rotation);
            Destroy(particle, 2.0f);
        }

        private void AddScore()
        {
            scoreQuiz += 100;
        }

        private void HideCanvas()
        {
            CanvasPrincipal.SetActive(false);
            CanvasAnswer.SetActive(false);
        }

        private void HideVirtualKeyboard()
        {
            virtualKeyboard.SetActive(false);
        }

        private void FinishQuiz()
        {
            SendToFirebase(scoreQuiz);
            HideVirtualKeyboard();
            HideCanvas();
            PlayForCorrectAnswer();
            ShowScore();
            StartCoroutine(WaitForLoadGame());
        }

        private IEnumerator WaitForLoadGame()
        {
            yield return new WaitForSecondsRealtime(2f);
            sceneController.StartRankingScene();
        }

        private void ChangeTimerDisplay()
        {
            initialSeconds--;
            TimeSpan timeWithMinutes = TimeSpan.FromSeconds( initialSeconds );

            string answer = string.Format("{0:D2}m:{1:D2}s", 
                    timeWithMinutes.Minutes, 
                    timeWithMinutes.Seconds);

            t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = answer.ToString();
        }

        public void SendToFirebase(int finalScore)
        {
            //string userName =  SaveUser.UserName.ToString();
            string userName = PlayerPrefs.GetString("USER_NAME");
            Debug.Log("SendToFire: " + userName);
            firebaseManager.ReceiveFinalQuizData(userName, finalScore);
        }

        void OnEnable() 
        {
            TimerCountdown.OnTimerFinished += FinishQuiz;
            TimerCountdown.OnTimerChange += ChangeTimerDisplay;
        }

        void OnDisable()
        {
            TimerCountdown.OnTimerFinished -= FinishQuiz;
            TimerCountdown.OnTimerChange -= ChangeTimerDisplay;
        }
    }
}