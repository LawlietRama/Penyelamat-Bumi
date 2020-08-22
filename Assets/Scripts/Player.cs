using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public FloatingJoystick floatingJoystick;
    public ButtonManager jumpButton;
    public ButtonManager interactButton;

    public bool stopMove;

    //anim
    public Animator anim;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    //public Vector3 startPosition;

    // Objects
    public Transform cam;
    CharacterController mover;
    public GameObject[] playerModel;
    public GameObject enemyHurtBox;

    // Input
    Vector2 input;
    private Vector3 moveDirection = Vector3.zero;

    // Camera
    Vector3 camF;
    Vector3 camR;

    // Physics
    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;
    public float speed = 10;
    float accel = 20;
    float turnSpeed = 35;
    float turnSpeedLow = 35;
    float turnSpeedHigh = 45;
    public float allowPlayerRotation = 0.1f;

    public Vector3 mExternalMovement = Vector3.zero;
    public Vector3 bounceMovement = Vector3.zero;

    public float bounceForceY = 8f;

    // Gravity
    float grav = 9.81f;
    [SerializeField]
    bool grounded = false;
    [SerializeField]
    bool isJumping = false;
    float inAirTime = 0;

    public float jumpTimeCounter;
    public float jumpTime;
    public bool holdJump = false;

    //Health
    public bool alive;
    public static int playerHealth = 100;
    public int damage = 1;

    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public float distToGround = 1f;

    public bool isBouncing;
    public float bouncingLength = .5f;
    private float bouncingCounter;
    public Vector3 bouncingPower;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        alive = true;
        holdJump = false;
        //transform.position = startPosition;
        anim = GetComponent<Animator>();
        mover = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && playerHealth > 0)
        {
            playerHealth--;
        }
        else if (other.tag == "Enemy")
        {
            playerHealth--;
            alive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnocking && !stopMove)
        {
            Move();
            mover.Move(velocity * Time.deltaTime);
        }

        if(isKnocking)
        {
            knockbackCounter -= Time.deltaTime;
            enemyHurtBox.SetActive(false);

            float yStore = velocity.y;
            velocity = (transform.forward * -knockbackPower.x);
            velocity.y = yStore;

            if(mover.isGrounded)
            {
                velocity.y = 0f;
            }

            velocity.y += Physics.gravity.y * 5f * Time.deltaTime;


            mover.Move(velocity * Time.deltaTime);

            if(knockbackCounter <= 0)
            {
                isKnocking = false;
                enemyHurtBox.SetActive(true);
            }
        }

        if (isBouncing)
        {
            bouncingCounter -= Time.deltaTime;

            if (mover.isGrounded)
            {
                velocity.y = 0f;
            }

            velocity.y += Physics.gravity.y * 5f * Time.deltaTime;


            mover.Move(velocity * Time.deltaTime);

            if (bouncingCounter <= 0)
            {
                isBouncing = false;
            }
        }

        if (stopMove == true)
        {
            velocity = Vector3.zero;
            //velocity.y += Physics.gravity.y * 5f * Time.deltaTime;
            velocity.y -= grav * Time.deltaTime;
            mover.Move(velocity);
        }
        
    }

    void Attack()
    {

    }

    void Interact()
    {

    }

    void Move()
    {
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();
    }




    void DoInput()
    {
        input = new Vector2(floatingJoystick.Horizontal, floatingJoystick.Vertical);
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);                                       //biar untuk pergerakan diagonal nilainya gak lebih dari 1... kan kita pengen bikin pergerakan kita berbentuk lingkaran bukan persegi...
        
        if (input.magnitude > allowPlayerRotation && grounded)
        {
            anim.SetFloat("magnitude", input.magnitude, StartAnimTime, Time.deltaTime);
        }
        else if (input.magnitude < allowPlayerRotation && grounded)
        {
            anim.SetFloat("magnitude", input.magnitude, StopAnimTime, Time.deltaTime);
        }
    }

    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        if(mExternalMovement != Vector3.zero)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
        else
        {
            grounded = mover.isGrounded;
        }
        
        //grounded = mover.isGrounded; //original

    }

    void DoMove()
    {
        intent = camF * input.y + camR * input.x;

        float tS = velocity.magnitude/speed;        //aslinya 5
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);
        if(input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward*input.magnitude*speed, accel*Time.deltaTime);

        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);

        
    }

    void DoGravity()
    {
        if (grounded)
        {
            anim.SetBool("isJumping", false);
            inAirTime = 0;
            velocity.y = -0.5f;
        }
        else if (!grounded)
        {
            inAirTime++;            
            anim.SetBool("isJumping", true);
            if(mExternalMovement == Vector3.zero)
            {
                anim.SetFloat("inAirTime", inAirTime * Time.deltaTime);
            }
            //velocity.y += Physics.gravity.y * 5f * Time.deltaTime; original
            velocity.y -= grav * Time.deltaTime;
        }
        //velocity.y = Mathf.Clamp(velocity.y, -9.8f, 9.8f); //original

        
        velocity.y = Mathf.Clamp(velocity.y, -10f, 10f);
    }

    void DoJump()
    {

        //if (grounded == true && Input.GetButtonDown("Jump"))
        if (grounded == true && jumpButton.pressed && holdJump == false)
        {
            holdJump = true;
            isJumping = true;
            anim.SetTrigger("takeOff");
            jumpTimeCounter = jumpTime;
            velocity.y = 15f;       //15f
                                   //animasi lompat biasa

        }
        //if (Input.GetButton("Jump") && isJumping == true)
        if (jumpButton.pressed && isJumping == true)
        {
            holdJump = true;
               //animasi menghempaskan kaki di udara
            if (jumpTimeCounter>0)
            {
                velocity.y = 15f;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                
                isJumping = false;
            }
        }
        //if(Input.GetButtonUp("Jump"))
        if(jumpButton.pressed == false)
        {
            isJumping = false;
            holdJump = false;
            
        }
           // Debug.Log(velocity.y);
        
       velocity.y += Physics.gravity.y * 5f * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (mExternalMovement != Vector3.zero)
        {
            mover.Move(mExternalMovement);
        }
        if (bounceMovement != Vector3.zero)
        {
            //bounceMovement.y += Physics.gravity.y * Time.deltaTime;
            mover.Move(bounceMovement);
        }
    }

    public Vector3 ExternalMovement
    {
        set
        {
            mExternalMovement = value;
        }
    }

    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        velocity.y = knockbackPower.y;
        mover.Move(velocity * Time.deltaTime);
    }

    public void Bounce()
    {
        isBouncing = true;
        bouncingCounter = bouncingLength;
        velocity.y = bouncingPower.y;
        //velocity.x = bounceForce;
        mover.Move(velocity * Time.deltaTime);
    }
}