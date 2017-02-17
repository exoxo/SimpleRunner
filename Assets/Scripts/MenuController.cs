using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public string[] SceneNames;

    public Canvas MainMenuCanvas;
    public Canvas MainSettingsMenu;

    public GameObject SceneManagerGO;

    public Text Score;
    public Text SoundStatus;

    CustomSceneManager customSceneManager;

    bool didInit;

    public void Init()
    {
        MainMenuCanvas.enabled = true;
        MainSettingsMenu.enabled = false;
        
        if (SceneManagerGO == null)
        {
            SceneManagerGO = GameObject.Find("SceneManager");
        }
        if (SceneManagerGO == null) Debug.Log("Cant find SceneManagerGO");

        customSceneManager = ExtendedGameObject.SetComponent<CustomSceneManager>(SceneManagerGO);
        customSceneManager.SetScenesArray(SceneNames);
        customSceneManager.Init();

        Score.text = GetHighScore().ToString();

        if (PlayerPrefs.GetInt("SRT_ALOWSOUND", 0) == 0)
            SoundStatus.text = "Sound OFF";
        else
            SoundStatus.text = "Sound ON";


        didInit = true;
    }

    public void Start()
    {
        Init();
    }

    public void OnPlayButton()
    {
        customSceneManager.LoadLevel(SceneNames[1]);
    }

    public void OnSettingsButton()
    {
        MainMenuCanvas.enabled = false;
        MainSettingsMenu.enabled = true;
        PlayerPrefs.SetInt("SRT_highScore", 1);
    }

    public void onBackToMenuButton()
    {
        MainMenuCanvas.enabled = true;
        MainSettingsMenu.enabled = false;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnSoundPushBtn()
    {
        if (SoundManager.Instance.IsSoundIsOff)
        {
            SoundManager.Instance.TurnOnSound();
            SoundStatus.text = "Sound ON";
        } else
        {
            SoundManager.Instance.TurnOffSound();
            SoundStatus.text = "Sound OFF";
        }
    }

    public float GetHighScore()
    {
        int high_score = PlayerPrefs.GetInt("SRT_highScore", 0);

        return high_score;

    }
}

