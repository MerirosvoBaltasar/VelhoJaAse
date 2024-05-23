using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float redTime;
    [SerializeField] private int playerLives;
    [SerializeField] private int playerHealthInspector;
    private int playerHealth;
    [SerializeField] private int playerDamage;
    [SerializeField] private bool playerIsDead;
    private Rigidbody2D playerBody;
    private Transform playerPosition;
    [SerializeField] private Transform spawnLocation;
    private SpriteRenderer playerSprite;
    private PlayerMovement playerMovement;
    private bool playerLoseLife;
    //[SerializeField] GameObject Background;
    //private SpriteRenderer BackgroundSprite;
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerPosition = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
        //BackgroundSprite = Background.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerLoseLife = false;
        playerHealth = playerHealthInspector;
        
    }

    void Update()
    {
        if(playerPosition.position.y <= - 80f)
        {
            StartCoroutine(PlayerDead());
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D playerHitCollision)
    {
        
        if(!playerLoseLife && playerHitCollision.CompareTag("EnemyBullet"))
        {
            playerHealth -= playerDamage;
            if(playerHealth <= 0)
            {
                playerLoseLife = true;
                playerLives -= 1;
                if(playerLives <= 0) { StartCoroutine(PlayerDead()); }
                else { StartCoroutine(PlayerLoseLifeGraphics()); }
            }
        }
    }

    IEnumerator PlayerLoseLifeGraphics()
    {
        for (int i = 0; i < 3; i++)
        {
            playerSprite.color = Color.red;
            yield return new WaitForSeconds(redTime);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(redTime);
        }
        playerLoseLife = false;
        playerHealth = playerHealthInspector;
    }
    IEnumerator PlayerDead()
    {
        playerMovement.enabled = false;
        playerPosition.position = spawnLocation.position;
        playerBody.constraints = RigidbodyConstraints2D.FreezePosition;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.7f);
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.3f);
            playerSprite.enabled = true;
        }
        playerBody.constraints = RigidbodyConstraints2D.None;
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerHealth = playerHealthInspector;
        playerMovement.enabled = true;
        playerBody.AddForce(Vector3.down * 3, ForceMode2D.Impulse);
    }
}
