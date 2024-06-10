using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //The time the playersprite flashes red when hit.
    [SerializeField] private float redTime;
    public bool playerDead;
    [SerializeField] private int playerMaxLives;
    public int playerLives;
    private Rigidbody2D playerBody;
    private Transform playerPosition;
    [SerializeField] private Transform spawnLocation;
    private SpriteRenderer playerSprite;
    private PlayerMovement playerMovement;
    private bool playerLoseLife;
    private Collider2D playerCollider;

    void Awake()
    {
        //Fetching the necessary components.
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerPosition = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        playerLives = playerMaxLives;
        playerLoseLife = false;
        playerDead = false;
    }
    void Update()
    {
        playerDead = playerLives <= 0 ? true : false;
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
            playerLives--;
            playerLoseLife = true;
            if(playerLives <= 0) { playerDead = true; OutOfLives(); return; }
            else { StartCoroutine(PlayerLoseLifeGraphics()); }
        }

        //If player touches a health item, add 1 to health.
        if(playerLives < playerMaxLives && playerHitCollision.CompareTag("HealthItem"))
        {
            playerLives++;   
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
        playerLives--;
        playerCollider.enabled = true;
        FreezePlayer();
        playerSprite.color = Color.white;

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.3f);
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSprite.enabled = true;
        }
        UnFreezePlayer();
        playerBody.AddForce(Vector3.down * 3, ForceMode2D.Impulse);
    }
    void FreezePlayer()
    {
        playerMovement.enabled = false;
        playerPosition.position = spawnLocation.position;
        playerBody.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    void UnFreezePlayer()
    {
        playerBody.constraints = RigidbodyConstraints2D.None;
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
    }
    //If player is out of lives, send him in the air, then disable his collider so that he will fall through the ground.
    void OutOfLives()
    {
        playerBody.AddForce(Vector3.up * 4, ForceMode2D.Impulse);
        playerSprite.color = Color.red;
        playerCollider.enabled = false; 
    }
}
