using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    //[SerializeField] private SceneController sceneController;
    private float delayWriter;
    private float delayWriterPT = 0.05f; //0.035f
    private float delayWriterEN = 0.07f; //0.035f
    private string welcomeEN = "Hello, welcome to the virtual reality experience in the framework of the Oleachain project. You are about to enter and experience full of fun and learn about olive growing and its related processes. Let's start with a quiz of questions, your score will increase if you answer the questions correctly. Are you ready?\nSo let's go!";
    //private string welcomePT = "Olá, muito bem vindo a experiência de realidade virtual no âmbito do projeto Oleachain. Você irá se divertir e aprender um pouco mais sobre a olivicultura e processos relacionados a cadeia de valor dos olivais. Iremos começar com um quiz de perguntas, a sua pontuação irá aumentar conforme você acertar as questões, está preparado? Então vamos lá!";
    private string welcomePT = "Olá, muito bem vindo a experiência de realidade virtual no âmbito do projeto Oleachain. Está prestes a entrar numa experiência de diversão e aprendizagem sobre a olivicultura e os processos a ela relacionados. Vamos começar com um quiz de perguntas, a sua pontuação irá aumentar se responder corretamente as questões. Está preparado? Então vamos lá!";

    public delegate void WelcomeSpeechFinished();
    public static event WelcomeSpeechFinished OnWelcomeSpeechFinished;

    void Start()
    {
        //sceneController = FindObjectOfType<SceneController>();

        //if (sceneController.GetSceneChonsen())
        if(PlayerPrefs.GetInt("LANGUAGE") == 1)
        {
            delayWriter = delayWriterEN;
            StartCoroutine("TypeWriter", welcomeEN);
        }
        else if (PlayerPrefs.GetInt("LANGUAGE") == 0)
        {
            delayWriter = delayWriterPT;
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
