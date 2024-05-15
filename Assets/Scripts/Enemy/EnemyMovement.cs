using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    private float enemyDirection;
    [SerializeField] private bool enemyNeedsToTurnAround;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Transform groundLevel;

    //Variables necessary to chack if enemy needs to turn around, and those for executing the actual turn.
    public bool enemyFacingRight;
    private LayerMask groundLayer;
    private Vector3 enemyHeightVector3;
    private Vector2 enemyHeight;
    private Vector3 enemyRotationDirection;

    //This is the horizontal component for the direction of the Raycast.
    private Vector2 rayCastHorizontal;
    private Vector2 rayCastDirection;
    //This is the tangent angle 
    private float rayCastAngleToTan;

    //Variables to verify if the player is within a certain distance. If so, the enemy will approach, then stop and shoot.
    //If player disappears from view, the enemy will look around for it.
    private Vector2 enemyPosition2D;

    //Variables needed to make the enemy notice the player.
   /* [SerializeField] private float enemyNoticeRadius;
    private bool enemyNoticePlayer;*/

    //Variables needed to make enemy stop and, if player disappears, look for it.
    [SerializeField] private bool itsTimeToShoot;
    public bool ItsTimeToShoot {get ; private set;}
    [SerializeField] private float enemyShootRadius;
    private LayerMask playerLayer;
    [SerializeField] private float enemyNoticeRadius;
    [SerializeField] private float castRadius;
    [SerializeField] private bool noticePlayer;
    [SerializeField] private float checkInterval;
    private bool lookForPlayer;

    void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");        
    }
    
    void Start()
    {
        //The value of tangent function for the angle of pi/2.
        rayCastAngleToTan = 1;
        //Calculate the vector from enemyPosition to groundLevel.
        enemyHeightVector3 = groundLevel.position - enemyPosition.position;
        enemyHeight = new Vector2(enemyHeightVector3.x, enemyHeightVector3.y);

        //Calculate the horizontal component of the raycast-vector (rayCastDirection).
        rayCastHorizontal = new Vector2(enemyHeight.magnitude * rayCastAngleToTan, 0);

        //Initialize the variables to check if the player is near.
        enemyPosition2D = enemyPosition.position;
        //Initialize the raycast to stop approaching the player and start shooting.
        itsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
        lookForPlayer = true;
    }

    void Update()
    {
        EnemyFacingRight();
        enemyPosition2D = enemyPosition.position;
        itsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
        if(!itsTimeToShoot)
        {
            MoveEnemy();
            NoticePlayer();
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

    void NoticePlayer()
    {
        if(lookForPlayer)
        {
            noticePlayer = Physics2D.Raycast(enemyPosition2D, transform.right * (-1), enemyNoticeRadius, playerLayer);
            if (noticePlayer) { RotateEnemy(); }
            lookForPlayer = false;
            StartCoroutine(EnemyWait());
        }
    }

    IEnumerator EnemyWait()
    {
        yield return new WaitForSeconds(checkInterval);
        lookForPlayer = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
        {
            if(!itsTimeToShoot)
            {
                RotateEnemy();
            }
        }
    }
}