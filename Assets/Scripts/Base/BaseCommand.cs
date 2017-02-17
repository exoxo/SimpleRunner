using UnityEngine;
using System.Collections;



public class BaseCommand : MonoBehaviour
{
    public virtual void Execute(Transform trans) { }
    public virtual void Execute(Transform tr, Vector3 srcTr, Vector3 dstTr) { }
}

