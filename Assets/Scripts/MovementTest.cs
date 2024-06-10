using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float horizontalInput;
    private float jumpTime;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Transform groundCheck;
    private Vector2 groundCheckPoint;
    private bool playerGrounded;
    private bool playerGroundedDelay;
    private LayerMask groundLayer;
    private bool jump;
    private int doubleJumpCounter;
    private float gravity => (-2f*jumpHeight)/Mathf.Pow(jumpTime/2f, 2);
    private float jumpForce => (2f*jumpHeight)/(jumpTime/2f);

    void Start()
    {
        groundCheckPoint = new Vector2(groundCheck.position.x, groundCheck.position.y);
        groundLayer = LayerMask.GetMask("Ground");
        doubleJumpCounter = 0;
    }

    void Update()
    {
        HorizontalMovement();
        GroundChecker();
        JumpMethod();
    }
    void HorizontalMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
    }

    void GroundChecker()
    {
        playerGrounded = Physics2D.Raycast(groundCheckPoint, Vector2.down, 0.5f, groundLayer);
        if(playerGrounded) { playerGroundedDelay = true; }
        if(playerGroundedDelay && !playerGrounded) { StartCoroutine(GroundedDelay()); }
    }
    IEnumerator GroundedDelay()
    {
        yield return null;
        playerGroundedDelay = playerGrounded? true : false;
    }
    void JumpMethod()
    {
        jump = Input.GetKey(KeyCode.Space);

        if(jump)
        {
            if(playerGrounded || playerGroundedDelay)
            {
                doubleJumpCounter = 0;
                GroundedJump();
            }

            if(doubleJumpCounter < 1 && !playerGrounded)
            {
                DoubleJump();
                doubleJumpCounter++;
            }
        }
    }
    void ApplyGravity()
    {
        float multiplier = playerBody.velocity.y < 0 ? 2f : 1f;
        if(!playerGrounded)
        {
            transform.position += new Vector3(0, gravity*multiplier, 0);
        }
    }
    void GroundedJump()
    {
        playerBody.AddForce(jumpForce*Vector3.up, ForceMode2D.Impulse);
    }
    void DoubleJump()
    {

    }
}
