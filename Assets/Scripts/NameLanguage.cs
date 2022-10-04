using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameLanguage : MonoBehaviour
{
    private SceneController sceneController;
    [SerializeField] private TextMeshProUGUI t_typeNameDescription;

    void Start()
    {
        sceneController = GameObject.FindObjectOfType<SceneController>();
        t_typeNameDescription = GameObject.Find("CanvasName/Panel/TextDescription").GetComponent<TextMeshProUGUI>();

        if(sceneController == null)
        {
            t_typeNameDescription.text = "No scene controller";
        } 
        else if(sceneController.GetSceneChonsen())
        {
            t_typeNameDescription.text = "Welcome! Please use your index finger to type your name\n When you finished you can press START_QUIZ";
        }
        else
        {
            t_typeNameDescription.text = "Bem vindo! Use o dedo indicador para digitar seu nome";
        }          
    }
}
