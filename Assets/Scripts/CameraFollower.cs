using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{

    public Transform followTarget;
    Vector3 targetOffset;
    public float moveSpeed = 1.5f;

    private Transform myTransform;

    bool isColliding;

    Vector3 normalPos = new Vector3(0, 2.55f, -4);
    Vector3 downPos = new Vector3(0, 1, -4.5f);
    
    void Start()
    {
        myTransform = transform;
        targetOffset = normalPos;
    }

    public void SetTarget(Transform aTransform)
    {
        followTarget = aTransform;
    }

    void LateUpdate()
    {
       // UpdateOffsetPosition();

        if (followTarget != null)
            myTransform.position = Vector3.Lerp(myTransform.position, followTarget.position + targetOffset, moveSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.tag == "Level")
        { 
            isColliding = true;
        }
        else
            isColliding = false;
    }

    public void UpdateOffsetPosition()
    {
        if(followTarget.position.x != myTransform.position.x)
            myTransform.position = new Vector3(followTarget.position.x, myTransform.position.y, myTransform.position.z);
        targetOffset = normalPos;
        /*
        if (isColliding)
            targetOffset = downPos;
        else
            targetOffset = normalPos;
            */
    }

}
