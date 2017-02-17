using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RandomPoolManager : PoolObjectManager
{
    FEController tempController;
    List<GameObject> listOfTargets;

    public override void Init()
    {
        //creating initial pool of objects
        for (int i = 0; i < Number; i++)
        {
            tempGO = Instantiate(listOfTargets[UnityEngine.Random.Range(0, listOfTargets.Count)], new Vector3(0,0,0), Quaternion.identity);
            if (ParentGO != null)
                tempGO.transform.SetParent(ParentGO.transform);
            if (tempGO.GetComponent<Rigidbody>() == null)
                tempGO.AddComponent<Rigidbody>();
            gameObjectList.Add(tempGO);
            tempGO.SetActive(false);
        }
        Debug.Log("Created " + Number + " objects");
    }

    public override GameObject GetObject()
    {
        int randVal = UnityEngine.Random.Range(0, gameObjectList.Count - 1);
        tempController = gameObjectList[randVal].GetComponent<FEController>();

        if (tempController != null && !tempController.IsMoving)
        {
            gameObjectList[randVal].SetActive(true);
            return gameObjectList[randVal];
        }
            
        else
        {
            return GetObject();
        }
    }

    public GameObject GetRandVisibleObject()
    {
        int randVal = UnityEngine.Random.Range(0, gameObjectList.Count - 1);
        if (!gameObjectList[randVal].activeSelf)
        {
            gameObjectList[randVal].SetActive(true);
            return gameObjectList[randVal];
        }
        else
        {
            return GetRandVisibleObject();
        }
    }

    public void SetTargetList(List<GameObject> listTargets)
    {
        listOfTargets = listTargets;
    }

    public List<GameObject> GetTargetList()
    {
        return listOfTargets;
    }
}

