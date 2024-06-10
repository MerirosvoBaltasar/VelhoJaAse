using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float playerDistanceToSpawn;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform playerPosition;
    private float distanceToPlayer;
    //Boolean to check if enemy spawning is in process.
    private bool timeToSpawn;
    //Boolean to check if the enemy is already spawned.
    private bool enemySpawned;
    void Start()
    {
        enemySpawned = false;
        //Method to check every second if player is near enough and an enemy should be spawned.
        InvokeRepeating("CheckIfPlayerAround", 5.0f, 1.0f); 
    }
    void Update()
    {
        
    }
    private void CheckIfPlayerAround()
    {
        if(!enemySpawned)
        {
            distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);
            //If the distance to the Player-gameobject is smaller than the prescribed spawning distance, instantiate enemy.
            timeToSpawn = distanceToPlayer < playerDistanceToSpawn;
            if(timeToSpawn) { SpawnEnemy(); enemySpawned = true; }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
