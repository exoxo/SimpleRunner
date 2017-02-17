using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallController : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if(other.gameObject.GetComponent<FEController>() != null)
            {
                other.gameObject.SetActive(false);
                other.gameObject.GetComponent<FEController>().StopFE();
            }
            else if(other.gameObject.tag == "Cloud")
                other.gameObject.SetActive(false);

        }
    }
}
