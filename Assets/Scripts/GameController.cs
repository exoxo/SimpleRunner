using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GameController : BaseGameController {

    public List<GameObject> TargetsFE;

    public List<GameObject> Clouds;
    CloudSpawning cloudSpawning;

    FEManager feManager;

    public GameObject PlayerPrefab;

    [System.NonSerialized]
    public Vector3 PointStartFE_GC = new Vector3(0, 0, 100f);
    [System.NonSerialized]
    public Vector3 PointEndFE_GC = new Vector3(0, 0, 0);
    [System.NonSerialized]
    public Vector3 SpeedVecFE_GC = new Vector3(0, 0, -15f);

    public int NumberOfFE;
    public GameObject FEParentGO;
    public GameObject CloudParentGO;

    Vector3 CloudStartingPoint = new Vector3(0,600, 6500);

    PlayerController focusPlayerScript;

    BaseUIDataManager uiDataManager;

    public Vector3 CameraOffSet_GC;

    public bool isFECanMove { get; private set; }

    public bool IsDebug;

    public readonly string PlayerScoreStringTag = "PlayerScore";

    public Text Score;
    public RawImage HP1;
    public RawImage HP2;
    public RawImage HP3;

    public AudioSource mainSound;

    static GameController instance;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        didInit = false;
        Init();
        isFECanMove = true;
    }

    public void Init()
    {
       // SpeedVecFE_GC = new Vector3(0, 0, -15f);
        feManager = gameObject.AddComponent<FEManager>();
        feManager.StartPointAllFE = PointStartFE_GC;
        feManager.EndPointAllFE = PointEndFE_GC;
        feManager.SpeedVecFE = PointEndFE_GC;
        feManager.ShouldExtend = false;
        feManager.TargetGameObjects = TargetsFE;
        feManager.PoolSizeFE = NumberOfFE;
        feManager.poolObjectManager.ParentGO = FEParentGO;

        feManager.Init();

        focusPlayerScript = ExtendedGameObject.SetComponent<PlayerController>(PlayerPrefab);
        focusPlayerScript.CameraOffSet = CameraOffSet_GC;
        focusPlayerScript.DataManager.SetHealth(3);


        uiDataManager = ExtendedGameObject.SetComponent<BaseUIDataManager>(gameObject);
        uiDataManager.ScoreField = Score;
        uiDataManager.Health1 = HP1;
        uiDataManager.Health2 = HP2;
        uiDataManager.Health3 = HP3;
        uiDataManager.player_lives = focusPlayerScript.DataManager.GetHealth();
        uiDataManager.gamePrefsName = "SRT";

        mainSound = ExtendedGameObject.SetComponent<AudioSource>(gameObject);
        if (PlayerPrefs.GetInt("SRT_ALOWSOUND", 0) == 0)
            mainSound.Stop();
        else
            mainSound.Play();

        #region CloudSystem Initializing and starting
        cloudSpawning = ExtendedGameObject.SetComponent<CloudSpawning>(gameObject);
        cloudSpawning.ParentCloud = CloudParentGO;
        cloudSpawning.CloudsToSpawn = Clouds;
        cloudSpawning.Init();
        cloudSpawning.TimeDelay = 5.5f;
        cloudSpawning.StartPoint = CloudStartingPoint;
        cloudSpawning.Speed = new Vector3(0, 0, -400f);
        cloudSpawning.StartCloudMovement();
        #endregion

        #region EventHandlers
        PlayerController.PlayerHittedLevel += PlayerLostLifeHandler;
        #endregion
        IsDebug = true;

        didInit = true;
    }

    public void Update()
    {
        feManager.SpeedVecFE = SpeedVecFE_GC;
        if (isFECanMove)
        {
            feManager.StartFEGen();
        }
        else
        {
            feManager.PauseAllFE();
        }
    }

    public void RestartGame()
    {
        PlayerController.PlayerHittedLevel -= PlayerLostLifeHandler;
        CustomSceneManager.Instance.RestartCurrentLevel();
        ShowRewardedAd();
    }

    IEnumerator PauseFEForTime(float time)
    {
        base.PlayerLostLife();
        isFECanMove = false;
        yield return new WaitForSeconds(time);
        focusPlayerScript.MakeInvisibleForSeconds(2f);
        isFECanMove = true;
    }

    public BaseUIDataManager GetUIDataManager()
    {
        if (uiDataManager != null)
            return uiDataManager;
        throw new NullReferenceException();
    }

    public void GoToMenu()
    {
        if (IsDebug) Debug.Log("Saving to PlayerPrefs player_highScore: " + uiDataManager.player_highscore);

        uiDataManager.SaveHighScore();

        PlayerController.PlayerHittedLevel -= PlayerLostLifeHandler;
        CustomSceneManager.Instance.LoadLevel(CustomSceneManager.Instance.MainMenuSceneName);
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }


    #region EventHandlers

    public void PlayerLostLifeHandler(object o, EventArgs e)
    {
        StartCoroutine(PauseFEForTime(3));
        if (focusPlayerScript.DataManager.GetHealth() == 1)
        {
            focusPlayerScript.GameFinished();
            uiDataManager.SaveHighScore();
            RestartGame();
        }

        focusPlayerScript.DataManager.ReduceHealth();
        uiDataManager.UpdateLives(focusPlayerScript.DataManager.GetHealth());

        if (IsDebug)
        {
            Debug.Log("Player lost life");
            Debug.Log("Current health is " + focusPlayerScript.DataManager.GetHealth());
        }
    }
    #endregion

}
