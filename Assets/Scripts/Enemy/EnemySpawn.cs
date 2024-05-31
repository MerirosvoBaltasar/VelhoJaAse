using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float playerDistanceToSpawn;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform playerPosition;
    private float distanceToPlayer;
    private bool timeToSpawn;
    private bool enemySpawned;
    void Start()
    {
        enemySpawned = false;
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
            timeToSpawn = distanceToPlayer < playerDistanceToSpawn;
            if(timeToSpawn) { SpawnEnemy(); enemySpawned = true; }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
