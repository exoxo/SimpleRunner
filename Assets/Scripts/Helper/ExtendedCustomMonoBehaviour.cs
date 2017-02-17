using UnityEngine;
using System.Collections;


public class ExtendedCustomMonoBehaviour : MonoBehaviour
{
    // This class is used to add some common variables to MonoBehaviour, rather than
    // constantly repeating the same declarations in every class.
    [System.NonSerialized]
    public Transform myTransform;
    [System.NonSerialized]
    public GameObject myGO;
    [System.NonSerialized]
    public Rigidbody myBody;

    [System.NonSerialized]
    public bool didInit;
    [System.NonSerialized]
    public bool canControl;

    [System.NonSerialized]
    int id;

    [System.NonSerialized]
    public Vector3 tempVEC;

    [System.NonSerialized]
    public Transform tempTR;

    public virtual void SetID(int anID)
    {
        id = anID;
    }

    public virtual int GetID()
    {
        return id;
    }
}