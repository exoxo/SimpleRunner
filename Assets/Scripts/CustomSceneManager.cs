using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    string[] SceneNames;

    public string MainMenuSceneName { get; set; }
    public string MainGamingScene { get; set; }

    Scene currentScene;

    bool didInit;

    static CustomSceneManager instance;
    public static CustomSceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string sceneName)
    {
        Application.LoadLevel(sceneName);
        currentScene = GetActiveScene();
    }

    public void LoadLevel(int levelNum)
    {
        LoadLevel(SceneNames[levelNum]);
    }

    public void RestartCurrentLevel()
    {
        Debug.Log("Current scene name: " + currentScene.name);
        Application.LoadLevel(GetActiveScene().name);
    }

    public void Init()
    {
        MainMenuSceneName = SceneNames[0];
        MainGamingScene = SceneNames[1];

        currentScene = SceneManager.GetActiveScene();

        didInit = true;
    }

    public void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void SetScenesArray(string[] newVal)
    {
        SceneNames = newVal;
    }

    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }
}

