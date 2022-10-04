using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = System.Random;
using System;

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

    [SerializeField] private GameLoad gameLoad;
    [SerializeField] private TextMeshProUGUI t_question;
    [SerializeField] private TextMeshProUGUI t_options;
    [SerializeField] private TextMeshProUGUI t_answerDescription;
    [SerializeField] private TextMeshProUGUI t_score;
    [SerializeField] private GameObject CanvasAnswer;
    [SerializeField] private GameObject CanvasScore;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private Transform referenceTransform;
    [SerializeField] private TextMeshProUGUI t_TimerDisplay;
    [SerializeField] private TimerCountdown timerCountdown;

    private AudioClip clip;
    private bool lockAnswer;
    private int round = 0;
    private int initialSeconds = 6;
    private List<QuizManager.Quiz> randomQuiz = new List<QuizManager.Quiz>();
    private SceneController sceneController;

    private List<string> question = new List<string>
    {
        "Qual dos seguintes instrumentos é utilizado na colheita tradicional?",
        "Onde se situa a oliveira mais antiga do mundo?",
        "Estima-se que a oliveira mais antiga do mundo tenha ...",
        "Qual o diâmetro do tronco da oliveira mais antiga do mundo?",
        "Em média, quantos quilos de azeitonas são precisos para produzir um litro de azeite virgem extra artesanal?",
        "O Homem já levou um ramo de oliveira até ...",
        "Quantas pessoas, em média, são precisas para abraçar a oliveira mais antiga de Portugal?",
        "Onde se situa a oliveira mais antiga de Portugal?",
        "Os ramos da oliveira são associados a:",
        "O caroço da azeitona pode ser utilizado para que fim?",
        "Quais são as variedades de oliveiras predominantes na região transmontana?",
        "No olival transmontano, qual é a área ocupada pela variedade Cobrançosa?",
        "Em que meses do ano ocorre, normalmente, a colheita da azeitona em Portugal?",
        "Qual das seguintes opções não é uma praga do olival?",
        "Quantas calorias têm, aproximadamente, 100 gramas de azeite?",
        "Qual é o azeite de menor qualidade?",
        "O azeite pode ter como finalidade a ...",
        "Qual destas opções representa a ordem correta do processo de transformação da azeitona em azeite?",
        "Quantas azeitonas foram produzidas em Trás-os-Montes no ano de 2019?",
        "Qual o período mais indicado para a prática da plantação das oliveiras?"
    };

    //timer 3 minutos

    private List<string> question_en = new List<string>
    {
        "Which of the following instruments is used in traditional harvesting?",
        "Where is the oldest olive tree in the world?",
        "The oldest olive tree in the world is estimated to have...",
        "What is the diameter of the trunk of the oldest olive tree in the world?",
        "On average, how many kilograms of olives does it take to produce a liter of artisanal extra virgin olive oil?",
        "Man once carried an olive branch to...",
        "How many people, on average, does it take to embrace the oldest olive tree in Portugal?",
        "Where is the oldest olive tree in Portugal?",
        "The branches of the olive tree are associated with:",
        "What can the olive pit be used for?",
        "What are the predominant varieties of olive trees in the Trás-os-Montes region?",
        "In the Trás-os-Montes olive grove, what is the area occupied by the Cobrançosa variety?",
        "In which months of the year does the olive harvest normally take place in Portugal?",
        "Which of the following is not a pest of the olive grove?",
        "How many calories are in approximately 100 grams of olive oil?",
        "What is the lowest quality olive oil?",
        "Olive oil can be used to ...",
        "Which of these options represents the correct order of the olive oil transformation process?",
        "How many olives were produced in Trás-os-Montes in 2019?",
        "What is the most suitable period for the practice of planting olive trees?"
    };

    private List<string> options = new List<string>
    {
        "A. Vara.\nB. Varejador elétrico.\nC. Trator.\nD. Foice.", //A
        "A. Portugal.\nB. Grécia.\nC. Espanha.\nD. Itália.", //B
        "A. ... de 3 a 5 mil anos.\nB. ... menos de 500 anos.\nC. ... de 6 a 9 mil anos.\nD. ... de 500 anos a 2000 anos.", //A
        "A. 1,50 metros.\nB. 3,56 metros.\nC. 5,10 metros.\nD. 4,60 metros.",
        "A. 2 quilos.\nB. 1 quilo.\nC.4 quilos.\nD. 6 quilos.",
        "A. ... Marte.\nB. ... Júpiter.\nC. ... à lua.\nD. ... ao sol.",
        "A. 5 pessoas.\nB. 33 pessoas.\nC. 12 pessoas.\nD. 7 pessoas.",
        "A. Gostei – Bragança.\nB. Cascalhos – Abrantes.\nC. Bemposta – Mogadouro.\nD. Arraiolos – Évora.",
        "A. Morte.\nB. Paz.\nC. Fertilidade.\nD. Pureza.",
        "A. Combustão.\nB. Alimentação humana.\nC. Fertilização de plantas.\nD. Lubrificação.",
        "A. Cordovil, Carrasquenha, Picual, Negrinha e Gordal.\nB. Picual, Arbequina, Leccino, Coratina e Frantoio.\nC. Verdeal, Madural, Negrinha, Santulhana e Cobrançosa.\nD. Coratina, Missão, Kalamata, Gordal e Verdeal.",
        "A. 70%.\nB. 50%.\nC. 20%.\nD. 10%.",
        "A. De Janeiro a Março.\nB. De Março a Maio.\nC. De Junho a Agosto.\nD. De Setembro a Dezembro.",
        "A. Mosca da azeitona (Bactrocera oleae).\nB. Tuberculose (Pseudomonas Savastanoi).\nC. Gafa (Colletotrichum acutatum).\nD. Traça (Prays Oleae).",
        "A. 900 kcal.\nB. 100 kcal.\nC. 650 kcal.\nD.220 kcal.",
        "A. Lampante.\nB. Virgem.\nC. Virgem extra.\nD. Puro.",
        "A. Combustão.\nB. Cosmética.\nC. Alimentação.\nD. Todas as anteriores.",
        "A. Colheita – transporte – limpeza – moenda – filtração – distribuição.\nB. Distribuição - moenda – transporte – limpeza – filtração – colheita.\nC. Colheita –transporte –moenda – limpeza– filtração – distribuição.\nD. Colheita – limpeza –transporte – filtração – moenda distribuição.",
        "A. De 300 000 a 500 000 toneladas.\nB. Mais de 900 000 toneladas.\nC. De 10 000 a 100 000 toneladas.\nD. De 600 000 a 800 000 toneladas.",
        "A. Setembro e outubro e/ou março e abril.\nB. Janeiro e fevereiro e/ou novembro e dezembro.\nC. Setembro e Outubro e/ou Janeiro e Fevereiro.\nD. Março e Abril e/ou Julho e Agosto."
    };

    private List<string> options_en = new List<string>
    {
        "A. Rod.\nB. Electric sweeper.\nC. Tractor.\nD. Sickle.", //A
        "A. Portugal.\nB. Greece.\nC. Spain.\nD. Italy.", //B
        "A. ... from 3 to 5 thousand years.\nB. ... less than 500 years.\nC. ... from 6 to 9 thousand years.\nD. ... from 500 years to 2000 years. ", //A
        "A. 1.50 meters.\nB. 3.56 meters.\nC. 5.10 meters.\nD. 4.60 meters.",
        "A. 2 kilos.\nB. 1 kilo.\nC.4 kilos.\nD. 6 kilos.",
        "A. ... Mars.\nB. ... Jupiter.\nC. ... to the moon.\nD. ... to the sun.",
        "A. 5 people.\nB. 33 people.\nC. 12 people.\nD. 7 people.",
        "A. Gostei – Bragança.\nB. Cascalhos – Abrantes.\nC. Bemposta – Mogadouro.\nD. Arraiolos – Évora.",
        "A. Death.\nB. Peace.\nC. Fertility.\nD. Purity.",
        "A. Combustion.\nB. Human food.\nC. Plant fertilization.\nD. Lubrication.",
        "A. Cordovil, Carrasquenha, Picual, Negrinha and Gordal.\nB. Picual, Arbequina, Leccino, Coratina and Frantoio.\nC. Verdeal, Madural, Negrinha, Santulhana and Cobrançosa.\nD. Coratina, Missão, Kalamata, Gordal and Verdeal.",
        "A. 70%.\nB. 50%.\nC. 20%.\nD. 10%",
        "A. From January to March.\nB. From March to May.\nC. From June to August.\nD. From September to December.",
        "A. Olive fly (Bactrocera oleae).\nB. Tuberculosis (Pseudomonas Savastanoi).\nC. Gafa (Colletotrichum acutatum).\nD. Moth (Prays Oleae).",
        "A. 900 kcal.\nB. 100 kcal.\nC. 650 kcal.\nD.220 kcal.",
        "A. Lampante.\nB. Virgin.\nC. Extra Virgin.\nD. Pure.",
        "A. Combustion.\nB. Cosmetics.\nC. Food.\nD. All of the above.",
        "A. Harvest – transport – cleaning – milling – filtration – distribution.\nB. Distribution - milling – transport – cleaning – filtration – harvesting.\nC. Harvesting – transport – milling – cleaning – filtration – distribution.\nD. Harvesting – cleaning – transport – filtration – milling distribution.",
        "A. From 300,000 to 500,000 tons.\nB. More than 900,000 tons.\nC. From 10,000 to 100,000 tons.\nD. From 600,000 to 800,000 tons.",
        "A. September and October and/or March and April.\nB. January and February and/or November and December.\nC. September and October and/or January and February.\nD. March and April and/or July and August."
    };
   
    private string[] answers = new string[20]
    {
        "A", "B", "A", "D", "D", "C", "A", "B", "B", "A", "C" , "C", "D", "B", "A", "A", "D", "A", "B", "A" 
    };

    private string[] answersDescription = new string[20]
    {
       "A - Vara.", "B - Grécia.", "A - De 3 a 5 mil anos.", "D - 4,60 metros.", "D - 6 quiilos.", "C - À lua.", "A - 5 pessoas.", "B - Cascalhos – Abrantes.", "B - Paz.", "A - Combustão.", "C - Verdeal, Madural, Negrinha, Santulhana e Cobrançosa." , "C - 20%.", "D - De Setembro a Dezembro.", "B - Tuberculosis (Pseudomonas Savastanoi).", "A - 900 kcal.", "A - Lampante.", "D - Todas as anteriores.", "A - Colheita – transporte – limpeza – moenda – filtração – distribuição.", "B - Mais de 900 000 toneladas.", "A - Setembro e Outubro e/ou Março e Abril." 
    };

    private string[] answersDescription_en = new string[20]
    {
       "A - Rod.", "B - Greece.", "A - From 3 to 5 thousand years.", "D - 4,60 meters.", "D - 6 kilos.", "C - To the moon.", "A - 5 people.", "B - Cascalhos – Abrantes.", "B - Peace.", "A - Combustion.", "C - Verdeal, Madural, Negrinha, Santulhana and Cobrançosa." , "C - 20%", "D - From September to December.", "B - Tuberculosis (Pseudomonas Savastanoi).", "A - 900 kcal.", "A - Lampante.", "D - All of the above.", "A - Harvest – transport – cleaning – milling – filtration – distribution.", "B -  More than 900,000 tons.", "A - September and October and/or March and April." 
    };

    private void Start() 
    {
        t_question = GameObject.Find("[CANVAS_QUIZ]/Panel/TextQuestion").GetComponent<TextMeshProUGUI>();
        t_options = GameObject.Find("[CANVAS_QUIZ]/Panel/TextOptions").GetComponent<TextMeshProUGUI>();
        t_answerDescription = GameObject.Find("[CANVAS_ANSWER]/Panel/TextAnswer").GetComponent<TextMeshProUGUI>();
        t_score = GameObject.Find("[CANVAS_SCORE]/Panel/TextScore").GetComponent<TextMeshProUGUI>();
        t_score.text = "0";
        t_TimerDisplay = GameObject.Find("[CANVAS_QUIZ]/Panel/BackgroungTimerPanel/TextTimerDisplay").GetComponent<TextMeshProUGUI>();
        t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + initialSeconds;
        timerCountdown = FindObjectOfType<TimerCountdown>();

        timerCountdown.SetInitialSecondsLeft(initialSeconds);

        CanvasAnswer = GameObject.Find("[CANVAS_ANSWER]");
        CanvasScore = GameObject.Find("[CANVAS_SCORE]");
        CanvasAnswer.SetActive(false);
        CanvasScore.SetActive(false);

        List<QuizManager.Quiz> listQuiz = new List<QuizManager.Quiz>();
        /*sceneController = GameObject.FindObjectOfType<SceneController>();

        if (sceneController.GetSceneChonsen()) //english
        {
            for(int i = 0; i<= (question_en.Count - 1); i++)    
            {
                listQuiz.Add(new QuizManager.Quiz(question_en.ElementAt(i), options_en.ElementAt(i), answers[i], answersDescription_en[i]));
            }
        }
        else //portuguese
        {
            for(int i = 0; i<= (question.Count - 1); i++)
            {
                listQuiz.Add(new QuizManager.Quiz(question.ElementAt(i), options.ElementAt(i), answers[i], answersDescription[i]));
            }
        }
        
        Random rng = new Random();
        randomQuiz = listQuiz.OrderBy(_ => rng.Next()).ToList();

        t_question.text = randomQuiz.ElementAt(0).Question;
        t_options.text = randomQuiz.ElementAt(0).Options;*/
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
                //t_answerDescription.color = Color.green;
                AddScore();
                PlayForCorrectAnswer();
                ShowScore();
            }
            else
            {
                //t_answerDescription.color = Color.green;
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
            gameLoad.LoadScene(4); //game mode
        }
    }

    private void ActivateParticle()
    {
        Vector3 particleY = new Vector3(0.0f, 0.1f, 0.0f);
        particleY += referenceTransform.transform.position;
        var particle = Instantiate(particleSystem, particleY, referenceTransform.transform.rotation);
        Destroy(particle, 2.0f);
    }

    private int scoreQuiz = 0; 

    private void AddScore()
    {
        scoreQuiz += 100;
    }

    private void FinishQuiz()
    {
        //Finish Quiz show score
        //PlaySound for finishing Quiz
        ShowScore();
    }

    private void ChangeTimerDisplay()
    {
        initialSeconds--;
        TimeSpan timeWithMinutes = TimeSpan.FromSeconds( initialSeconds );

        string answer = string.Format("{0:D2}m:{1:D2}s", 
                timeWithMinutes.Minutes, 
                timeWithMinutes.Seconds);

        //t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + timeWithMinutes;
        t_TimerDisplay.GetComponent<TextMeshProUGUI>().text = answer.ToString();

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
