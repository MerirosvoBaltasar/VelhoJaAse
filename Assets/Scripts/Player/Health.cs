using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //The time the playersprite flashes red when hit.
    [SerializeField] private float redTime;

    [SerializeField] public int playerLives;
    [SerializeField] private bool playerIsDead;
    private Rigidbody2D playerBody;
    private Transform playerPosition;
    [SerializeField] private Transform spawnLocation;
    private SpriteRenderer playerSprite;
    private PlayerMovement playerMovement;
    private bool playerLoseLife;

    void Awake()
    {
        //Fetching the necessary components.
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerPosition = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        playerLoseLife = false;
    }
    void Update()
    {
        //If the player falls for to long, execute the 'PlayerFallToDeath' function.
        if(playerPosition.position.y <= - 60f)
        {
            StartCoroutine(PlayerFallToDeath());
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D playerHitCollision)
    {
        //If the player is hit by 'EnemyBullet' and currently not losing a life (that is, flashing red), make him lose a life.
        if(!playerLoseLife && playerHitCollision.CompareTag("EnemyBullet"))
        {
            if(playerLives <= 0) { OutOfLives(); return; }
            playerLives -= 1;
            playerLoseLife = true;
            StartCoroutine(PlayerLoseLifeGraphics());
        }
    }

    IEnumerator PlayerLoseLifeGraphics()
    {
        //The player sprite turns red and returns to normal. Happens twice.
        for (int i = 0; i < 2; i++)
        {
            playerSprite.color = Color.red;
            yield return new WaitForSeconds(redTime);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(redTime);
        }
        playerLoseLife = false;
    }
    IEnumerator PlayerFallToDeath()
    {
        //Disable first the playerMovement-script. Then, transfer the gameObject to the spawn location and freeze its position
        //for a while. Player-sprite flashes on and off 2 times, then is dropped and the constraints of the rigidbody removed.
        playerMovement.enabled = false;
        playerLives--;
        playerPosition.position = spawnLocation.position;
        playerBody.constraints = RigidbodyConstraints2D.FreezePosition;
        playerSprite.color = Color.white;

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.3f);
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSprite.enabled = true;
        }
        playerBody.constraints = RigidbodyConstraints2D.None;
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
        playerBody.AddForce(Vector3.down * 3, ForceMode2D.Impulse);
    }
    //If player is out of lives, send him in the air, then disable his collider so that he will fall through the ground.
    void OutOfLives()
    {
        playerBody.AddForce(Vector3.up * 3, ForceMode2D.Impulse);
        StartCoroutine(OutOfLivesFall());
    }
    IEnumerator OutOfLivesFall()
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerBody.isKinematic = true;
        yield return new WaitForSeconds(2f);
        playerBody.isKinematic = false;
    }
}
