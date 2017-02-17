using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainSound;
    public string SoundPrefs = "SRT_ALOWSOUND";


    static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject.GetComponent(instance.GetType()));
    }

    public void Start()
    {
        mainSound = ExtendedGameObject.SetComponent<AudioSource>(gameObject);
        if (mainSound == null)
            Debug.Log("SoundManager: cant find mainSound");
        if (!PlayerPrefs.HasKey(SoundPrefs))
            PlayerPrefs.SetInt(SoundPrefs, 1);

        if (PlayerPrefs.GetInt(SoundPrefs, 0) == 0)
            mainSound.Stop();
        else
            mainSound.Play();
    }

    public bool IsSoundIsOff { get; set; }

    
    public void TurnOnSound()
    {
        Debug.Log("Turning on all sounds");
        mainSound.Play();
        PlayerPrefs.SetInt(SoundPrefs, 1);
        IsSoundIsOff = false;
    }

    public void TurnOffSound()
    {
        Debug.Log("Turning off all sounds");
        mainSound.Stop();
        PlayerPrefs.SetInt(SoundPrefs, 0);
        IsSoundIsOff = true;
    }

    public void RestartSound()
    {
        if (PlayerPrefs.GetInt(SoundPrefs, 0) == 0)
            return;
        else
        {
            TurnOffSound();
            TurnOnSound();
        }
    }
    
}

