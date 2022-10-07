using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [SerializeField] private SceneController sceneController;
    private float delayWriter = 0.035f;
    private string welcomeEN = "Hello, welcome to the virtual reality experience within the framework of the Oleachain project. You will have fun and learn a little more about olive growing and related processes. We will start with a quiz of questions, your score will increase as you get the questions right, are you ready? So let's go!";
    private string welcomePT = "Olá, muito bem vindo a experiência de realidade virtual no âmbito do projeto Oleachain. Você irá se divertir e aprender um pouco mais sobre a olivicultura e processos relacionados a cadeia de valor dos olivais. Iremos começar com um quiz de perguntas, a sua pontuação irá aumentar conforme você acertar as questões, está preparado? Então vamos lá!";
    public delegate void WelcomeSpeechFinished();
    public static event WelcomeSpeechFinished OnWelcomeSpeechFinished;
    
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();

        if (sceneController.GetSceneChonsen())
        {
            delayWriter = 0.044f;
            StartCoroutine("TypeWriter", welcomeEN);
        }
        else
        {
            delayWriter = 0.044f;
            StartCoroutine("TypeWriter", welcomePT);
        }           
    }

    public IEnumerator TypeWriter(string TypeWriter)
    {
        textUI.text = "";

        for (int letter = 0; letter < TypeWriter.Length; letter++ )
        {
            textUI.text = textUI.text + TypeWriter[letter];
            yield return new WaitForSeconds(delayWriter);
        }

        yield return new WaitForSeconds(3f);

        OnWelcomeSpeechFinished();
    }
}
