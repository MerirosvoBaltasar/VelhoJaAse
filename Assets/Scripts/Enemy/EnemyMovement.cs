using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    private float enemyDirection;
    private bool enemyNeedsToTurnAround;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Transform groundLevel;
    private Vector2 enemyPosition2D;

    //Variables necessary to check if enemy is on the edge of the block, and those for rotating it if necessary.
    [HideInInspector] public bool enemyFacingRight;
    private LayerMask groundLayer;
    private Vector3 enemyHeightVector3;
    private Vector2 enemyHeight;
    private Vector3 enemyRotationDirection;

    //This is the horizontal component for the direction of the Raycast to check if the enemy is on the edge of the block.
    private Vector2 rayCastHorizontal;
    private Vector2 rayCastDirection;

    //Variables needed to make enemies notice if the player is behind them and to shoot, if player is near enough.
    [HideInInspector] public bool ItsTimeToShoot;
    [SerializeField] private float enemyShootRadius;
    private LayerMask playerLayer;
    [SerializeField] private float enemyNoticeRadius;
    private bool noticePlayer;
    [SerializeField] private float enemyNoticeInterval;
    private bool enter;

    // float distance = Vector2.Distance(playerPosition.position, transform.position);

    void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");        
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
    }
    
    void Start()
    {
        //Calculate the vector from enemyPosition to groundLevel.
        enemyHeightVector3 = groundLevel.position - enemyPosition.position;
        enemyHeight = new Vector2(enemyHeightVector3.x, enemyHeightVector3.y);

        //Calculate the horizontal component of the raycast-vector (rayCastDirection).
        rayCastHorizontal = new Vector2(enemyHeight.magnitude, 0);

        //Initialize the variables to check if the player is near.
        enemyPosition2D = enemyPosition.position;
        InvokeRepeating("NoticePlayer", 0.5f, 0.1f);
    }

    void Update()
    {
        EnemyFacingRight();
        EnemyShootCheck();
        enemyPosition2D = enemyPosition.position;
    }
    void FixedUpdate()
    {
        if(!ItsTimeToShoot)
        {
            MoveEnemy();
        }
    }

    void EnemyFacingRight()
    {
        enemyFacingRight  = (enemyPosition.right == Vector3.right) ? true : false;
        //Initialize the rotation direction.
        enemyRotationDirection = enemyFacingRight ? Vector3.left : Vector3.right;
    }

    void EnemyOnEdge()
    {
        //Make the Raycast conform to the direction the enemy is facing at.
        rayCastDirection = enemyFacingRight ? enemyHeight + rayCastHorizontal : enemyHeight + rayCastHorizontal * (-1);


        //Enemy needs to turn around when the Raycast returns 'false', that is, when there is no ground ahead of it.
        enemyNeedsToTurnAround = !(Physics2D.Raycast(enemyPosition.position, rayCastDirection, rayCastDirection.magnitude, groundLayer));

        if(enemyNeedsToTurnAround)
        { RotateEnemy(); }
    }

    void RotateEnemy()
    {
        transform.rotation *= Quaternion.FromToRotation(transform.right, enemyRotationDirection);
    }

    void MoveEnemy()
    {
        EnemyOnEdge();
        enemyDirection = enemyFacingRight ? 1.0f : -1.0f;
        enemyPosition.position += new Vector3(enemyDirection*enemySpeed*Time.deltaTime, 0, 0);
    }

    //Coroutine to introduce delay into the rotation of the enemy if it notices the player behind it.
    IEnumerator TurningDelay(float waitTime)
    {
        enter = true;
        yield return new WaitForSeconds(waitTime);
        RotateEnemy();
        enter = false;
    }

    //Method to check if player is behing the enemy.
    void NoticePlayer()
    {
        noticePlayer = Physics2D.Raycast(enemyPosition2D, transform.right * (-1), enemyNoticeRadius, playerLayer);
        if (!enter && noticePlayer)
        {
            StartCoroutine(TurningDelay(enemyNoticeInterval));
        }
        if (!enter && !noticePlayer)
        {
            StopCoroutine(TurningDelay(enemyNoticeInterval));
        }
    }

    //Method to check if enemy is near enough for the enemy to start shooting.
    void EnemyShootCheck()
    {
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
    }
}