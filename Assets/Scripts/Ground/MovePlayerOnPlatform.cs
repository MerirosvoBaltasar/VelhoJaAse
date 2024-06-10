using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnPlatform : MonoBehaviour
{
    private bool playerIsOnPlatform;
    [SerializeField] GameObject Player;
    private Rigidbody2D playerBody;
    private Transform playerPosition;

    void Start()
    {
        playerBody = Player.GetComponent<Rigidbody2D>();
        playerPosition = Player.GetComponent<Transform>();
    }

    void Update()
    {
        if(playerIsOnPlatform)
        {
            CheckIfPlayerJumps();
        }
    }
    void CheckIfPlayerJumps()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            playerBody.isKinematic = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            playerPosition.parent = this.transform;
            playerBody.isKinematic=true;
            playerIsOnPlatform = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            playerPosition.parent = null;
            playerBody.isKinematic = false;
            playerIsOnPlatform = false;
        }
    }
}
