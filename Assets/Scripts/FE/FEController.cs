using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class FEController : ExtendedCustomMonoBehaviour
{
    public bool isMoving;
    public bool IsMoving { get { return isMoving; } private set { isMoving = value; } }
    public bool CollisionWithSmth;

    public Vector3 Speed;

    public void Start()
    {
        didInit = false;
    }

    public void Init()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myBody.isKinematic = false;
        didInit = true; 
    }

    public void StartFE(Vector3 spVec)
    {
        Speed = spVec;
        myBody.isKinematic = false;
        IsMoving = true;
        myBody.velocity = spVec;
    }

    public void StopFE()
    {
        IsMoving = false;
        myBody.isKinematic = true;
    }

    public void PauseFE()
    {
        myBody.isKinematic = false;
        myBody.velocity = Vector3.zero;
    }

    public void ResumeFE()
    {
        StartFE(Speed);
    }
}

