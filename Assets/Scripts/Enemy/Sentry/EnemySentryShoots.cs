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
    
    //'shootAfterPlayerDisappearsDelay' is the variable for how long the gun should keep shooting after the player disappears.
    //'delay' is the current waiting time which is compared to the variable 'shootAfterPlayerDisappearsDelay'.
    [SerializeField] private float shootAfterPlayerDisappearsDelay;
    [SerializeField] float delay;
    private LayerMask playerLayer;
    //The boolean telling if the ggun should shoot or not.
    [SerializeField] private bool gunShouldShoot;
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
        delay = 0;
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
    //Method to scan if the player is near enough to shoot the bullet or not. If the player has entered
    //the gun's field of vision and disappears, the gun will continue shooting for the prescribed time.
    private bool SentryScan()
    {
        //'timeToShootRaw' is the raw value for whether the player is visible. 'gunShouldShoot' dictates if the gun should shoot or not.

        bool timeToShootRaw = Physics2D.Raycast(transform.position, Vector3.right * (-1.0f), sentryNoticeDistance, playerLayer); 
        
        //If the 'gunShouldShoot' variable already equals the raycast result, return 'delay' to zero. 
        if (gunShouldShoot == timeToShootRaw)
        { delay = 0; }

        //If the raycast returns 'true', that is, if the player is visible, start shooting immidiately.
        if (timeToShootRaw) { gunShouldShoot = timeToShootRaw; }

        //If the raycast returns 'false' but 'gunShouldShoot' is true, keep shooting for a certain time
        //and only thereafter make 'gunShouldShoot' equal to the raycast.
        if(!timeToShootRaw && gunShouldShoot)
        {
            delay += Time.deltaTime * 10;
            if(delay > shootAfterPlayerDisappearsDelay)
            {
                gunShouldShoot = timeToShootRaw;
            }
        }
        return gunShouldShoot;
    }
}
