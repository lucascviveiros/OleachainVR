using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip[] audioClips_en = new AudioClip[7];
    [SerializeField] private AudioClip[] audioClips_pt = new AudioClip[7];
    [SerializeField] private SceneController sceneController;
    private bool language;

    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        audioSource = GetComponent<AudioSource>();

        //Audio English
        audioClips_en[0] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_welcome_en");
        audioClips_en[1] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_1_en");
        audioClips_en[2] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_2_en");
        audioClips_en[3] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_3_en");
        audioClips_en[4] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_fim_en");
        audioClips_en[5] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_nice");
        audioClips_en[6] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_game_tutorial_en");

        //Audio Portuguese
        audioClips_pt[0] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_welcome_pt");
        audioClips_pt[1] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_1_pt");
        audioClips_pt[2] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_2_pt");
        audioClips_pt[3] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_3_pt");
        audioClips_pt[4] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_fim_pt");
        audioClips_pt[5] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_voice_mtbem");
        audioClips_pt[6] = Resources.Load<AudioClip>("Prefabs/Voice/rosa_game_tutorial_pt");

        if(sceneController.GetCurrentScene() != "GAME_OCULUS_SDK")
        {

        if (sceneController.GetSceneChonsen())
        {
            WelcomeSpeechEN();

        }
        else
            WelcomeSpeechPT();
        
        }


        else
        {
            //Tutorial chamado pelo speech game
        }

        
    }

    public void WelcomeSpeechPT()
    {
        audioSource.PlayOneShot(audioClips_pt[0]);
    }

    public void WelcomeSpeechEN()
    {
        audioSource.PlayOneShot(audioClips_en[0]);
    }

    public void Voice1()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[1]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[1]);
        }
    }

    public void Voice2()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[2]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[2]);
        }
    }

    public void Voice3()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[3]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[3]);
        }
    }

    public void VoiceEnd()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[4]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[4]);
        }
    }

    public void VoiceNice()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[5]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[5]);
        }
    }

    public void TutorialPT_EN()
    {
        if (sceneController.GetSceneChonsen())
        {
            audioSource.PlayOneShot(audioClips_en[6]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_pt[6]);
        }
    }

}
