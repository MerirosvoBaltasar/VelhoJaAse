using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables concerning jumping.
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float startFallSpeed;
    public GroundCheck Ground;
    int jumpCounter;
    [SerializeField] Rigidbody2D playerBody;
    private bool jumpKey;
    
    //Variables concerning the movement of the gameobject.
    [SerializeField] private float moveSpeed;
    private float horizontalMove;
    [SerializeField] Transform playerPosition;

    //Variables concerning the rotation of the gameobject
    public bool facingRight;
    private bool goingRight;
    private bool goingLeft;
    private Vector3 rotationDirection;
    
    void Awake()
    {
        //Initialize rotation variables.
        facingRight = true;
        goingLeft = false;
        goingRight = false;

        //Initialize jump variables.
        jumpCounter = 0;
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }
    void Update()
    {
        MovePlayer();
        //Fetch information from GroundCheck-script to see if gameobject is grounded.
        bool OnGround = Ground.isGrounded;
        Jump(OnGround);
    }
    
    //Method to move the gameobject from left to right. Calls also the method to rotate the gameobject.
    private void MovePlayer()
    {
       horizontalMove = Input.GetAxis("Horizontal");
       transform.position += new Vector3(horizontalMove*moveSpeed*Time.deltaTime, 0, 0);   

        RotationCheck(horizontalMove);
    }

    //Method to check if the gameobject needs to be rotated. If needed, the method rotates the gameobject 180 degrees.
    private void RotationCheck(float horizontalMove)
    {
        //Check if the gameobject's x-axis aligns with that of the world space.
        facingRight = transform.right == Vector3.right ? true : false;

        //"going"-variables return "true" when the user starts pressing the arrow key associated with the variable.
        goingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D);
        goingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A);
        
        //If user presses left arrow when gameobject is facing right, the gameobject is rotated.
        if(facingRight && goingLeft)
        {
            RotatePlayer();
        }
        //If user presses right arrow when gameobject is facing left, the gameobject is rotated.
        if(!facingRight && goingRight)
        {
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        rotationDirection = facingRight ? Vector3.left : Vector3.right;
        transform.rotation *= Quaternion.FromToRotation(transform.right, rotationDirection);
    }

    //Jump method is responsable for sending the gameobject into the air and dropping it.
    private void Jump(bool onGround)
    {
        //Get the instance when the player presses space to make the gameobject jump.
        jumpKey = Input.GetKeyDown(KeyCode.Space);

        //If gameobject is on the ground, return double jump counter to zero.
        if(onGround) { jumpCounter = 0;}

        if (jumpKey)
        {
            //If the gamobject is grounded, it will jump.
            if(onGround)
            {
                playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            
            //If the gameobject is not on the ground, it can jump one more time,
            //thus creating the possibility for a double jump.
            if(!onGround && (jumpCounter < 1))
            {
                playerBody.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
                jumpCounter++;
            }

        }

        //Check if the gameobject is falling, that is, if it is not on the ground and
        //it's y-velocity is equal or less than zero. If so, make it fall faster with fallspeed up to the limit of maxFallSpeed.
        if (!onGround)
        {
            if(playerBody.velocity.y <= startFallSpeed && playerBody.velocity.y > maxFallSpeed)
            {
                playerBody.AddForce(Vector2.down * fallSpeed, ForceMode2D.Force);
            }
        }
    }
}
