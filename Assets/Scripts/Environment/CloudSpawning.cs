using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawning : MonoBehaviour
{
    public List<GameObject> CloudsToSpawn;

    public GameObject ParentCloud;
    RandomPoolManager poolObjectManager = new RandomPoolManager();

    GameObject tempGO;
    Rigidbody rb;

    public Vector3 StartPoint { get; set; }
    public Vector3 Speed { get; set; }
    bool canSpawn;
    public float TimeDelay { get; set; }
    bool didInit;

    public void Init()
    {
        if (ParentCloud != null)
            poolObjectManager.ParentGO = ParentCloud;
        if (CloudsToSpawn != null)
        {
            poolObjectManager.SetTargetList(CloudsToSpawn);
            poolObjectManager.Number = 20;
            Debug.Log("Object setting success");
            poolObjectManager.Init();
        }
        else
        {
            Debug.Log("Cant initialize CloudSpawning");
            didInit = false;
            return;
        }

        foreach (GameObject v in poolObjectManager.gameObjectList)
        {
            rb = ExtendedGameObject.SetComponent<Rigidbody>(v);
            rb.useGravity = false;
            rb.mass = 0.01f;
        }

        didInit = true;
    }

    void Update()
    {
        if (canSpawn && didInit)
        {
            tempGO = poolObjectManager.GetRandVisibleObject();
            tempGO.transform.position = StartPoint;
            tempGO.transform.rotation  = Quaternion.Euler(0,-90, 0);
            tempGO.GetComponent<Rigidbody>().velocity = Speed;

            canSpawn = false;

            Invoke("ReloadSpawn", TimeDelay);
        }
    }

    public void StartCloudMovement()
    {
        canSpawn = true;
    }

    void ReloadSpawn()
    {
        canSpawn = true;
    }
}

