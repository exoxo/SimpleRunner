using UnityEngine;
using System.Collections;

public class SpawnController : ScriptableObject
{
    private ArrayList playerTransforms;
    private ArrayList playerGameObjects;

    private Transform tempTrans;
    private GameObject tempGO;

    private GameObject[] playerPrefabList;
    private Vector3[] startPositions;
    private Quaternion[] startRotations;

    // singleton structure based on AngryAnt's fantastic wiki entry over at http://wiki.unity3d.com/index.php/Singleton

    private static SpawnController instance;

    public SpawnController()
    {
        if (instance != null)
        {
            return;
        }

        // as no instance already exists, we can safely set instance to this one
        instance = this;
    }

    public static SpawnController Instance
    {
        get
        {
            if (instance == null)
            {
                // no instance exists yet, so we go ahead and create one
                ScriptableObject.CreateInstance<SpawnController>(); // new SpawnController ();
            }
            // now we pass the reference to this instance back to the other script so it can communicate with it
            return instance;
        }
    }

    public void Restart()
    {
        playerTransforms = new ArrayList();
        playerGameObjects = new ArrayList();
    }

    public void SetUpPlayers(GameObject[] playerPrefabs, Vector3[] playerStartPositions, Quaternion[] playerStartRotations, Transform theParentObj, int totalPlayers)
    {
        playerPrefabList = playerPrefabs;
        startPositions = playerStartPositions;
        startRotations = playerStartRotations;

        // call the function to take care of spawning all the players and putting them in the right places
        CreatePlayers(theParentObj, totalPlayers);
    }

    public void CreatePlayers(Transform theParent, int totalPlayers)
    {
        playerTransforms = new ArrayList();
        playerGameObjects = new ArrayList();

        for (int i = 0; i < totalPlayers; i++)
        {
            tempTrans = Spawn(playerPrefabList[i], startPositions[i], startRotations[i]);

            if (theParent != null)
            {
                tempTrans.parent = theParent;
                tempTrans.localPosition = startPositions[i];
                tempTrans.localRotation = startRotations[i];
            }

            playerTransforms.Add(tempTrans);

            playerGameObjects.Add(tempTrans.gameObject);
        }
    }

    public GameObject GetPlayerGO(int indexNum)
    {
        return (GameObject)playerGameObjects[indexNum];
    }

    public Transform GetPlayerTransform(int indexNum)
    {
        return (Transform)playerTransforms[indexNum];
    }

    public Transform Spawn(GameObject anObject, Vector3 aPosition, Quaternion aRotation)
    {
        // instantiate the object
        tempGO = (GameObject)Instantiate(anObject, aPosition, aRotation);
        tempTrans = tempGO.transform;

        // return the object to whatever was calling
        return tempTrans;
    }

    public GameObject SpawnGO(GameObject anObject, Vector3 aPosition, Quaternion aRotation)
    {
        // instantiate the object
        tempGO = (GameObject)Instantiate(anObject, aPosition, aRotation);
        tempTrans = tempGO.transform;

        // return the object to whatever was calling
        return tempGO;
    }

    public ArrayList GetAllSpawnedPlayers()
    {
        return playerTransforms;
    }
}