using UnityEngine;
using System.Collections;

public class BaseSideLook : ExtendedCustomMonoBehaviour
{
    Vector3 moveDirection;
    bool isGrounded;

    public float maxSpeed;
    public float jumpForce;

    bool doubleJump;
    public bool facingRight = true;

    public Transform groundCheck;
    float groundRadius = 0.01f;
    public LayerMask whatIsGround;

    public Keyboard_Input default_input;

    Animator anim;

    public float horz;
    public float vert;

    void Awake()
    {
        moveDirection = transform.TransformDirection(Vector3.forward);
        anim = GetComponent<Animator>();
    }

    public virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        myGO = gameObject;
        myBody = GetComponent<Rigidbody>();
        myTransform = transform;

        default_input = myGO.AddComponent<Keyboard_Input>();
    }

    public virtual void SetUserInput(bool value)
    {
        canControl = value;
    }

    public virtual void GetInput()
    {
        horz = Mathf.Clamp(default_input.GetHorizontal(), -1, 1);
        vert = Mathf.Clamp(default_input.GetVertical(), -1, 1);
    }

    //Updating input parameters
    public virtual void LateUpdate()
    {
        if (canControl)
            GetInput();
    }

    //Core function that moves target child object, that implements this class
    void FixedUpdate()
    {
        if (!canControl)
            Input.ResetInputAxes();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (isGrounded)
            doubleJump = false;

        UpdateMovement();

        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(horz));
    }

    public virtual void UpdateMovement()
    {
        //Horizontal movement stuff
        if (horz > 0.1 || horz < -0.1)
            myBody.velocity = new Vector2(horz * maxSpeed, myBody.velocity.y);
        if (horz > 0 && !facingRight)
            Flip();
        else if (horz < 0 && facingRight)
            Flip();

        //Jumping stuff
        if ((isGrounded || !doubleJump) && Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Ground", false);
            myBody.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !isGrounded)
                doubleJump = true;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = myTransform.localScale;
        theScale.x *= -1;
        myTransform.localScale = theScale;
    }

    
}

