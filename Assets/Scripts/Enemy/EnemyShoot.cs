using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform enemyGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float enemyShootInterval;
    public EnemyMovement enemyMovement;
    public bool timeToShoot;
    private bool shotInterval;

    void Start()
    {
        shotInterval = true;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    void Update()
    {
        timeToShoot = enemyMovement.ItsTimeToShoot;
        if(timeToShoot)
        {
            CreateEnemyBullet();
        }
    }

    //Function to initialize the bullet prefab into the scene.
    void CreateEnemyBullet()
    {
        if(shotInterval)
        {
            shotInterval = false;
            GameObject spawnedBullet = Instantiate(enemyBullet, enemyGunMuzzle.position, Quaternion.identity);
            spawnedBullet.GetComponent<EnemyBullet>().Enemy = gameObject;
            StartCoroutine(EnemyFire());
        }
    }

    IEnumerator EnemyFire()
    {
        yield return new WaitForSeconds(enemyShootInterval);
        shotInterval = true;
    }
}
