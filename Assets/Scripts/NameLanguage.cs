using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OleaChainVR
{
   // This class change the description of the text in type_name scene according to the language choosed in the language scene
    public class NameLanguage : MonoBehaviour
    {
        private SceneController sceneController;
        [SerializeField] private TextMeshProUGUI t_typeNameDescription;

        void Start()
        {
            sceneController = GameObject.FindObjectOfType<SceneController>();
            t_typeNameDescription = GameObject.Find("CanvasName/Panel/TextDescription").GetComponent<TextMeshProUGUI>();

            if(PlayerPrefs.GetInt("LANGUAGE") == 1)            
            {
                t_typeNameDescription.text = "Welcome! Please use your index finger to type your name\nWhen you're finished, you can press START QUIZ button";
            }
            else
            {
                t_typeNameDescription.text = "Bem vindo! Use o dedo indicador para digitar seu nome\nQuando terminar, pressione o botão STAR QUIZ";
            }

            //StartQuizButton(); //Debugging 
        }

        public void StartQuizButton()
        {
            sceneController.StartQuiz();
        }
    }
}
