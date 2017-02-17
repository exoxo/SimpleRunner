using System;
using UnityEngine;
using System.Collections;

public class ExtendedGameObject : ScriptableObject
{
    public static T SetComponent<T>(GameObject go) where T : Component
    {
        if (go == null)
        {
            Debug.Log("ExtendedGameObject: SetComponent cant be null");
            new NullReferenceException();
            return null;
        }
        T tempT;
        tempT = go.GetComponent<T>();
        if (tempT != null)
            return tempT;
        else
        {
            tempT = go.AddComponent<T>();
            return tempT;
        }
    }
}
