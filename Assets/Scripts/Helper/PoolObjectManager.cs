using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolObjectManager : MonoBehaviour {

    public List<GameObject> gameObjectList = new List<GameObject>();

    public GameObject TargetObject;
    public GameObject ParentGO;
    public int Number = 35;
    public bool ShouldExtend;

    protected GameObject tempGO;

    public virtual void Init()
    {
        //creating initial pool of objects
        for (int i = 0; i < Number; i++)
        {
            tempGO = Instantiate(TargetObject, TargetObject.transform.position, Quaternion.identity);
            if(ParentGO != null)
                tempGO.transform.SetParent(ParentGO.transform);
            if (tempGO.GetComponent<Rigidbody>() == null)
                tempGO.AddComponent<Rigidbody>();
            gameObjectList.Add(tempGO);
            tempGO.SetActive(false);
        }
        Debug.Log("Created " + Number + " objects");
    }

    public virtual GameObject GetObject()
    {
        for (int i = 0; i < Number; i++)
        {
            if (!gameObjectList[i].activeSelf)
            {
                gameObjectList[i].SetActive(true);
                return gameObjectList[i];
            }
                
        }

        //if there is not too much objects in pool, creating another one
        if (ShouldExtend)
        {
            tempGO = Instantiate(TargetObject, TargetObject.transform.position, Quaternion.identity);
            Number += 1;
            gameObjectList.Add(tempGO);
            return tempGO;
        }
        else
            return null;
    }
	
}
