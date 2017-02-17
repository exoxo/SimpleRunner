using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseUIDataManager : MonoBehaviour
{
    [System.NonSerialized]
    public int player_score;
    [System.NonSerialized]
    public int player_lives;
    [System.NonSerialized]
    public int player_highscore;

    [HideInInspector]
    public Text ScoreField;
    [HideInInspector]
    public RawImage Health1;
    [HideInInspector]
    public RawImage Health2;
    [HideInInspector]
    public RawImage Health3;

    public string gamePrefsName = "SRT"; 

    public int MaximumHealth { get; set; }

    public void Start()
    {
        MaximumHealth = 3;
        LoadHighScore();
    }

    public void UpdateScore(int aScore)
    {
        player_score += aScore;
        if (player_score > player_highscore)
        {
            if (GameController.Instance.IsDebug) Debug.Log("New highScore: " + player_highscore);
            player_highscore = player_score;
            SaveHighScore();
        }
    }

    public void UpdateLives(int alifeNum)
    {
        player_lives = alifeNum;
    }

    public void AddLife(int alifeNum)
    {
        if (player_lives < MaximumHealth)
            player_lives += alifeNum;
    }

    public void LoadHighScore()
    {
        // grab high score from prefs
        if (PlayerPrefs.HasKey(gamePrefsName + "_highScore"))
        {
            player_highscore = PlayerPrefs.GetInt(gamePrefsName + "_highScore");
        }
    }

    public void SaveHighScore()
    {
        // as we know that the game is over, let's save out the high score too
        if(GameController.Instance.IsDebug) Debug.Log("Save new highScore");
        PlayerPrefs.SetInt(gamePrefsName + "_highScore", player_highscore);
    }

    public void LateUpdate()
    {
        UpdateScore();
        UpdateHP();
    }

    public void UpdateScore()
    {
        ScoreField.text = player_score.ToString();
    }

    public void UpdateHP()
    {
        switch(player_lives)
        {
            case 0:
                Health1.enabled = false;
                Health2.enabled = false;
                Health3.enabled = false;
                break;
            case 1:
                Health1.enabled = true;
                Health2.enabled = false;
                Health3.enabled = false;
                break;
            case 2:
                Health1.enabled = true;
                Health2.enabled = true;
                Health3.enabled = false;
                break;
            case 3:
                Health1.enabled = true;
                Health2.enabled = true;
                Health3.enabled = true;
                break;

        }
    }
}