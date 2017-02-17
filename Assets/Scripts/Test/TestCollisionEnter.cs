using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionEnter : MonoBehaviour {
  
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Some collision");
        if (other != null)
            Debug.Log("Object collision with smth");
    }
    
    public void Update()
    {
        OnTriggerEnter(new Collider());
    }
    public void OnTriggerEnter(Collider other)
    { 
        if (other != null)
            Debug.Log("Object trigger with smth");
    }
}
