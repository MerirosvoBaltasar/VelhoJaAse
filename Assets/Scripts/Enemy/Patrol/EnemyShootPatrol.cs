using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootPatrol : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform enemyGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float enemyShootInterval;
    public EnemyMovement2 enemyMovement;
    public bool timeToShoot;
    [SerializeField] private bool shootTimer;
    private bool enter;


    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement2>();
    }
    void Update()
    {
        timeToShoot = enemyMovement.ItsTimeToShoot;
        if(!enter && timeToShoot)
        {
            StartCoroutine(WaitAndShoot());
        }
    }

    IEnumerator WaitAndShoot()
    {
        enter = true;
        GameObject spawnedBullet = Instantiate(enemyBullet, enemyGunMuzzle.position, Quaternion.identity);
        spawnedBullet.GetComponent<EnemyBullet1>().Enemy = gameObject;
        yield return new WaitForSeconds(enemyShootInterval);
        enter = false;
    }
}