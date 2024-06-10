using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySentryShoots : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform sentryGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject sentryBullet;
    //The time the sentry gun waits between shots.
    [SerializeField] private float sentryShootInterval;
    [SerializeField] private float sentryNoticeDistance;
    private LayerMask playerLayer;
    //The boolean telling if the player is near or not.
    private bool timeToShoot;
    //boolean to block entering the coroutine while it's still executing.
    private bool waitBeforeShooting;
    //Boolean to tell the bullet which direction it should fly to.
    public bool enemyFacingRight;


    void Start()
    {
        //Initialize variables and invoke the method to operate the gun (every x seconds).
        enemyFacingRight = false;
        waitBeforeShooting = false;
        InvokeRepeating("SentryGunOperation", 3f, 0.5f);
        playerLayer = LayerMask.GetMask("Player");
    }
    void Update()
    {
    }

    void SentryGunOperation()
    {
        if(!waitBeforeShooting)
        {
            if (SentryScan())
            {
                StartCoroutine(WaitAndShoot());
            }

        }
    }
    //Coroutine to fire bullets every certain time interval.
    IEnumerator WaitAndShoot()
    {
        waitBeforeShooting = true;
        GameObject spawnedBullet = Instantiate(sentryBullet, sentryGunMuzzle.position, Quaternion.identity);
        spawnedBullet.GetComponent<SentryBullet>().Enemy = gameObject;
        yield return new WaitForSeconds(sentryShootInterval);
        waitBeforeShooting = false;
    }
    //Method to scan if the player is near enough to shoot the bullet or not.
    private bool SentryScan()
    {
        timeToShoot = Physics2D.Raycast(transform.position, Vector3.right * (-sentryNoticeDistance), playerLayer); 
        return timeToShoot;
    }
}
