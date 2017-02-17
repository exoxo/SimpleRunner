using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BaseGameController : ExtendedCustomMonoBehaviour
{
    bool paused;
    public GameObject explosionPrefab;

    public virtual void PlayerLostLife()
    {
        // deal with player life lost (update U.I. etc.)
    }

    public virtual void SpawnPlayer()
    {
        // the player needs to be spawned
    }

    public virtual void Respawn()
    {
        // the player is respawning
    }

    public virtual void StartGame()
    {
        // do start game functions
    }

    public void Explode(Vector3 aPosition)
    {
        // instantiate an explosion at the position passed into this function
        Instantiate(explosionPrefab, aPosition, Quaternion.identity);
    }

    public virtual void RestartGameButtonPressed()
    {
        // deal with restart button (default behaviour re-loads the currently loaded scene)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
    }

    public bool Paused
    {
        get
        {
            // get paused
            return paused;
        }
        set
        {
            // set paused 
            paused = value;

            if (paused)
            {
                // pause time
                Time.timeScale = 0f;
            }
            else
            {
                // unpause Unity
                Time.timeScale = 1f;
            }
        }
    }
}

