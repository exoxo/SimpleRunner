using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class FEManager : BaseFEManager
{
    [System.NonSerialized]
    public RandomPoolManager poolObjectManager = new RandomPoolManager();

    List<FEController> listOfFEControllers = new List<FEController>();

    public List<GameObject> TargetGameObjects;

    public bool CanGen;
    public bool IsPaused;
    bool wasPaused;

    public int PoolSizeFE { get; set; }
    public bool ShouldExtend { get; set; }

    FEController tempFEScript;
    List<FEController> movingFE = new List<FEController>();

    public static int FEID;


    public void Start()
    {
        IsInit = false;
        
        //It should be Inited by GameController
    }

    public void Init()
    {
        poolObjectManager.Number = PoolSizeFE;
        poolObjectManager.ShouldExtend = ShouldExtend;

        prevGO = new GameObject();
        prevGO.name = "PrevGO";
        prevGO.transform.SetParent(poolObjectManager.ParentGO.transform);
        prevGO.transform.position = StartPointAllFE - StartPointAllFE / 2;

        if (TargetGameObjects != null)
            poolObjectManager.SetTargetList(TargetGameObjects);
        else
        {
            Debug.Log("FEManager: Set FETarget!");
            IsInit = false;
            return;
        }
        poolObjectManager.Init();

        foreach (var v in poolObjectManager.gameObjectList)
        {
            tempFEScript = v.AddComponent<FEController>();
            listOfFEControllers.Add(tempFEScript);
            tempFEScript.Init();
            tempFEScript.SetID(FEID++);
        }

        tempGO = poolObjectManager.GetObject();
        tempGO.transform.position = StartPointAllFE;
        tempFEScript = tempGO.GetComponent<FEController>();

        IsInit = true;
    }

    public override void StartFEGen()
    {
        IsPaused = false;
        CanGen = true;
    }

    public override void StopFEGen()
    {
        CanGen = false;
    }

    public override void PauseAllFE()
    {
        IsPaused = true;
    }

    public override void ResumeAllFE()
    {
        IsPaused = false;
        Debug.Log("ResumeAllFE");
        prevGO.GetComponent<FEController>().Start();
    }

    public override void MoveNext()
    {
        
        if (!CanGen)
            return;

        if (IsPaused)
        {
            movingFE.Clear();
            foreach (var v in listOfFEControllers)
            {
                v.PauseFE();
                int i = 0;
                if (v.isMoving)
                    movingFE.Add(v);
            }

            wasPaused = true;
            return;
        }
       
        if (prevGO.transform.position.z < tempGO.transform.position.z - tempGO.GetComponent<Collider>().bounds.size.z || wasPaused)
        {
            //Starting all FE that already was in way
            if (wasPaused)
            {
                foreach (FEController v in movingFE)
                    v.StartFE(SpeedVecFE);
            }

            Debug.Log("set speed to FEController");
            tempFEScript.StartFE(SpeedVecFE);
            prevGO = tempGO;

            tempGO = poolObjectManager.GetObject();
            tempGO.transform.position = StartPointAllFE;
            tempFEScript = tempGO.GetComponent<FEController>();
            wasPaused = false;
        }
        
    }
}

