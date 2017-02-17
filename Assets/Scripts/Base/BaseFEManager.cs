using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class BaseFEManager : MonoBehaviour
{
    public bool IsInit { get; set; }
    public float MovingSpeed { get; set; }

    [System.NonSerialized]
    public Vector3 StartPointAllFE;
    [System.NonSerialized]
    public Vector3 EndPointAllFE;
    [System.NonSerialized]
    public Vector3 SpeedVecFE;


    [System.NonSerialized]
    public GameObject tempGO;
    [System.NonSerialized]
    public GameObject prevGO;
    [System.NonSerialized]
    public Transform tempTR;

    public virtual void PauseAllFE()
    {
        
    }

    public virtual void ResumeAllFE()
    {

    }

    public virtual void StartFEGen()
    {
        
    }

    public virtual void StopFEGen()
    {
        
    }

    public virtual void ReloadAllFE()
    {
        
    }

    public virtual void MoveNext()
    {

    }

    void FixedUpdate()
    {
        MoveNext();
    }
}

