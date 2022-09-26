using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = System.Random;

public class QuizManager : MonoBehaviour
{
    public class Quiz
    {
        private string answer;
        private string question;
        private string options;

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

        public Quiz(string question, string options, string answer)
        {
            this.question = question;
            this.options = options;
            this.answer = answer;
        }
    }

    private int round = 0;
    [SerializeField] private  GameLoad gameLoad;
    [SerializeField] private TextMeshProUGUI t_question;
    [SerializeField] private TextMeshProUGUI t_options;
    [SerializeField] private TextMeshProUGUI t_debugg;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private Transform referenceTransform;
    private AudioClip clip;
    private bool lockAnswer;
    private List<QuizManager.Quiz> randomQuiz = new List<QuizManager.Quiz>();

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
   
    private string[] answers = new string[20]
    {
        "A", "B", "A", "D", "D", "C", "A", "B", "B", "A", "C" , "C", "D", "B", "A", "A", "D", "A", "B", "A" 
    };

    private void Start() 
    {
        t_question = GameObject.Find("CanvasQuiz/Panel/TextQuestion").GetComponent<TextMeshProUGUI>();
        t_options = GameObject.Find("CanvasQuiz/Panel/TextOptions").GetComponent<TextMeshProUGUI>();

        List<QuizManager.Quiz> listQuiz = new List<QuizManager.Quiz>();

        for(int i = 0; i<= (question.Count - 1); i++)
        {
            listQuiz.Add(new QuizManager.Quiz(question.ElementAt(i), options.ElementAt(i), answers[i]));
        }

        Random rng = new Random();
        randomQuiz = listQuiz.OrderBy(_ => rng.Next()).ToList();
        
        foreach (var item in randomQuiz)
        {
            Debug.Log("Q: " + item.Question + " A: " + item.Answer);
        }

        t_question.text = randomQuiz.ElementAt(0).Question;
        t_options.text = randomQuiz.ElementAt(0).Options;

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
                PlayForCorrectAnswer();
            }
            else
            {
                PlayForWrongAnswer();
            }

            lockAnswer = true;
            NextRound();
            StartCoroutine(WaitForNextAnswer());
        }
    }

    private IEnumerator WaitForNextAnswer()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        lockAnswer = false;
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
            gameLoad.LoadScene(1);
        }
    }

    private void ActivateParticle()
    {
        Vector3 particleY = new Vector3(0.0f, 0.1f, 0.0f);
        particleY += referenceTransform.transform.position;
        var particle = Instantiate(particleSystem, particleY, referenceTransform.transform.rotation);
        Destroy(particle, 2.0f);
    }

}
