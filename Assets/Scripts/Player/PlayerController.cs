using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

class PlayerController : BasePlayerManager
{
    TouchInput touchInput;
    Keyboard_Input keyboardInput;

    CameraFollower cameraFollower;

    List<string> bonusNames = new List<string>()
    {
        "Coin",
        "InvisibleBonus",
        "Life",
        "Level"
    };

    public GameObject MainCameraGO;
    public Vector3 CameraOffSet;

    Animator anim;

    Transform myTransform;
    bool canMove;

    bool canJumpAgain;
    bool canSlideAgain;
    bool canRollLeftAgain;
    bool canRollRightAgain;

    bool isInvisible;

    CapsuleCollider capCollider;

    #region Events
    public static event EventHandler PlayerHittedLevel;
    #endregion

    public void Start()
    {
        didInit = false;
        Init();
    }

    void Init()
    {
        myTransform = transform;
        canMove = true;

        canJumpAgain = true;
        canSlideAgain = true;
        canRollLeftAgain = true;
        canRollRightAgain = true;

#if UNITY_EDITOR
        keyboardInput = ExtendedGameObject.SetComponent<Keyboard_Input>(gameObject);
#elif UNITY_ANDROID
        touchInput = ExtendedGameObject.SetComponent<TouchInput>(gameObject);
#endif

        anim = ExtendedGameObject.SetComponent<Animator>(gameObject);

        MainCameraGO = GameObject.Find("Main Camera");
        if (MainCameraGO == null)
        {
            Debug.Log("Cant find main camera");
            return;
        }
        cameraFollower = ExtendedGameObject.SetComponent<CameraFollower>(MainCameraGO);
        cameraFollower.followTarget = gameObject.transform; 

        capCollider = ExtendedGameObject.SetComponent<CapsuleCollider>(gameObject);
        base.Init();

        MakeUninvisible();

        didInit = true;
    }

    public void LateUpdate()
    {
        if (!didInit)
            return;

        //For animated model, than want to run away every frame
        myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, 18);
    }

    public void Update()
    {
        if(!didInit)
            return;

        UpdatePlayerMovement();

        Debug.Log("Is player invisible: " + isInvisible);
    }

    public void UpdatePlayerMovement()
    {
        if (!GameController.Instance.isFECanMove)
            return;

#if UNITY_EDITOR
        if (keyboardInput.horz > 0 && myTransform.position.x != 2)
        {
            if (canRollRightAgain)
            {
                anim.Play("RollRight");
                StartCoroutine(GetRight(myTransform));
                canRollRightAgain = false;
                canRollLeftAgain = false;
            }
            
        }
        else if (keyboardInput.horz < 0 && myTransform.position.x != -2)
        {
            if (canRollLeftAgain)
            {
                StartCoroutine(GetLeft(myTransform));
                anim.Play("RollLeft");
                canRollLeftAgain = false;
                canRollRightAgain = false;
            }
        }
        else if (keyboardInput.vert > 0)
        {
            Jump(myTransform);
        }
        else if (keyboardInput.vert < 0)
        {
            SlideDown(myTransform);
        }
        else { }
#elif UNITY_ANDROID
            if (touchInput.horz > 0 && myTransform.position.x != 2){
                 if (canRollRightAgain)
                {
                    anim.Play("RollRight");
                    StartCoroutine(GetRight(myTransform));
                    canRollRightAgain = false;
                    canRollLeftAgain = false;
                }
            }
            else if (touchInput.horz < 0 && myTransform.position.x != -2){
                if (canRollLeftAgain)
                {
                    StartCoroutine(GetLeft(myTransform));
                    anim.Play("RollLeft");
                    canRollLeftAgain = false;
                    canRollRightAgain = false;
                }
            }
            else if (touchInput.vert > 0)
            {
                Jump(myTransform);
                anim.Play("CustomRPGJump");
            }
            else if (touchInput.vert < 0)
            {
                SlideDown(myTransform);
            }
            else{}
#endif
    }

    public Collider GetPlayerCollider()
    {
        return capCollider;
    }

    public void Jump(Transform tr)
    {
        if (canJumpAgain)
        {
            anim.Play("CustomRPGJump");
            StartCoroutine(MoveUp(tr));
            canJumpAgain = false;
        }
        
    }

    public void SlideDown(Transform tr)
    {   
        if(canSlideAgain)
        {
            canSlideAgain = false;
            anim.Play("Roll-Forward");
            capCollider.center = new Vector3(capCollider.center.x, 0.15f, capCollider.center.z);
            Invoke("SetPlayerColliderOnDefault", 0.8f);
            //MakeInvisibleForSeconds(0.5f);
            //StartCoroutine(GetDown(tr));
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!CheckTagExistance(other.tag, bonusNames))
            return;
        GameController.Instance.IsDebug = true;
        switch (other.tag)
        {
            case "Level":
                if (GameController.Instance.IsDebug) Debug.Log("Level detected");
                if (!isInvisible && PlayerHittedLevel != null)
                {
                    PlayerHittedLevel(this, EventArgs.Empty);
                    anim.Play("Damaged");
                }
                break;
            case "Coin":
                if (GameController.Instance.IsDebug) Debug.Log("Picked up coin");
                base.DataManager.AddScore(10);
                GameController.Instance.GetUIDataManager().UpdateScore(10);
                if (GameController.Instance.IsDebug) Debug.Log("Current highScore is: " + DataManager.GetHighScore());
                Destroy(other.gameObject);
                break;
            case "Life":
                if (GameController.Instance.IsDebug) Debug.Log("Picked up Life");
                base.DataManager.AddHealth(1);
                GameController.Instance.GetUIDataManager().AddLife(1);
                
                if (GameController.Instance.IsDebug) Debug.Log("New Health: " + GameController.Instance.GetUIDataManager().player_lives);
                Destroy(other.gameObject);
                break;
        }

    }

    bool CheckTagExistance(string tagName, List<string> aList)
    {
        foreach (string s in aList)
        {
            if (tagName == s)
                return true;
        }
        return false;
    }

    public void MakeInvisibleForSeconds(float time)
    {
        isInvisible = true;
        Invoke("MakeUninvisible", time);
    }

    void MakeUninvisible()
    {
        isInvisible = false;
    }

    void SetPlayerColliderOnDefault()
    {
        capCollider.center = new Vector3(0, 0.84f, 0.14f);
        canSlideAgain = true;
    }


    #region JumpIEnumerator
    IEnumerator MoveUp(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.y < startPosition.y + 2)
        {
            tr.position += new Vector3(0, 7, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(transform.position.x, 2.21f, transform.position.z);
        StartCoroutine(MoveDown(transform));
    }

    IEnumerator MoveDown(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.y > startPosition.y - 2)
        {
            tr.position -= new Vector3(0, 5, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(transform.position.x, 0.21f, transform.position.z);
        canJumpAgain = true;
    }
    #endregion

    #region SlideIEnumerator
    IEnumerator GetDown(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.y > startPosition.y - 0.42f)
        {
            tr.position -= new Vector3(0, 1.5f, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(transform.position.x, -0.21f, transform.position.z);

        StartCoroutine(GetUp(tr));
    }

    IEnumerator GetUp(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.y < startPosition.y + 0.42f)
        {
            tr.position += new Vector3(0, 0.6f, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(transform.position.x, 0.21f, transform.position.z);
        canSlideAgain = true;
    }
    #endregion

    #region LeftAndRightIEnumerators
    IEnumerator GetLeft(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.x > startPosition.x - 2f)
        {
            tr.position -= new Vector3(9f, 0, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(startPosition.x -2f, transform.position.y, transform.position.z);
        canRollLeftAgain = true;
        canRollRightAgain = true;
    }

    IEnumerator GetRight(Transform tr)
    {
        Vector3 startPosition = tr.position;
        while (tr.position.x < startPosition.x + 2f)
        {
            tr.position += new Vector3(9f, 0, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector3(startPosition.x + 2f, transform.position.y, transform.position.z);
        canRollLeftAgain = true;
        canRollRightAgain = true;
    }
    #endregion
}

