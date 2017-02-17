using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingLerp : MonoBehaviour {

    public GameObject targetGO;

    public Transform StartP;
    public Transform EndP;
    public float TimeGain = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Time.deltaTime);
        targetGO.GetComponent<Rigidbody>().velocity = new Vector3(TimeGain, 0, 0);
	}
}
