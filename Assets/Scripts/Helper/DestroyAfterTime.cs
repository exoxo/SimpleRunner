using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float time = 0.3f;
    
	void Start () {
		Invoke("DestroyObject", time);
	}

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
