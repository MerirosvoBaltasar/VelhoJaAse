using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBullet : MonoBehaviour
{
    //This variable refers to the Enemy-gameobject. The reference is possible because it was set upon initializing
    //the bullet-prefab (done from the 'EnemySoots'-script').
    public GameObject Enemy;
    //By referring to the 'Enemy'-gameobject, it is possible to check which direction the shooter is facing when firing the bullet.
    [SerializeField] private bool shooterFacingRight;
    private float bulletDirection;

    [SerializeField] private float bulletSpeed;

    //Variables needed to determine the lifetime of the bullet.
    private float initializationTime;
    private float updateTime;
    [SerializeField] private float bulletLifeTime;

    void Awake()
    {
        initializationTime = Time.timeSinceLevelLoad;
    }
    void Start()
    {
        //Check which direction the player is facing when firing the bullet.
        ShooterFacingRightFunction();
    }

    void Update()
    {
        MoveBullet(bulletDirection);
        BulletLifeTimeFunction();
    }

    //Function to move the bullet to the specified direction.
    void MoveBullet(float direction)
    {
        transform.position += new Vector3(bulletSpeed * direction * Time.deltaTime, 0, 0);
    }
    //Function to check which direction the shooter is facing at. This influences the direction of the bullet.
    void ShooterFacingRightFunction()
    {
        shooterFacingRight = Enemy.GetComponent<EnemySentryShoots>().enemyFacingRight;
        bulletDirection = shooterFacingRight ? 1.0f : -1.0f;
    }
    //If the bullet has existed for longer than the time specified by 'bulletLifeTime', destroy it.
    void BulletLifeTimeFunction()
    {
        updateTime = Time.timeSinceLevelLoad;

        if (bulletLifeTime < updateTime - initializationTime)
        {
            Destroy(gameObject);
        }
    }
    //If the bullet hits something, destroy it.
    void OnCollisionEnter2D(Collision2D bulletHit)
    {
        if (updateTime - initializationTime > 0.02f)
        {
            Destroy(gameObject);
        }
    }
}
