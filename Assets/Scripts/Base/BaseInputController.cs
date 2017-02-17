using UnityEngine;
using System.Collections;

public class BaseInputController : MonoBehaviour
{

    // directional buttons
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;

    // fire / action buttons
    public bool Fire1;

    // weapon slots
    public bool Slot1;
    public bool Slot2;
    public bool Slot3;
    public bool Slot4;
    public bool Slot5;
    public bool Slot6;
    public bool Slot7;
    public bool Slot8;
    public bool Slot9;

    public float vert;
    public float horz;
    public bool shouldRespawn;

    public Vector3 TEMPVec3;

    public virtual void CheckInput()
    {
        // override with your own code to deal with input
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    public virtual float GetHorizontal()
    {
        return horz;
    }

    public virtual float GetVertical()
    {
        return vert;
    }

    public virtual bool GetFire()
    {
        return Fire1;
    }

    public bool GetRespawn()
    {
        return shouldRespawn;
    }


}