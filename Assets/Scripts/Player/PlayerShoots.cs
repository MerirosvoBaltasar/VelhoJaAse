using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform playerGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject playerBullet;
    //Check if shoot-key is pressed.
    [SerializeField] private bool playerShoots;

    void Update()
    {
        CreatePlayerBullet();
    }

    //Function to initialize the bullet prefab into the scene.
    void CreatePlayerBullet()
    {
        playerShoots = Input.GetKeyDown(KeyCode.R);

        //If shoot-key is pressed, initialize the bullet prefab into the specified location.
        //Also, create a reference to this gameobject into the 'PlayerBullet'-script of the initialized gameobject.
        //This is done in order to assign to the bullet the direction where the player is facing upon shooting.
        if(playerShoots)
        {
            GameObject spawnedBullet = Instantiate(playerBullet, playerGunMuzzle.position, Quaternion.identity);

            spawnedBullet.GetComponent<PlayerBullet>().Player = gameObject;

        }

    }
}
