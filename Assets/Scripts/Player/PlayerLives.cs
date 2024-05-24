using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] List<GameObject> playerLives = new List<GameObject>();
    private bool playerLosesLives;
    private int playerLivesCount;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();

    }
    void Start()
    {
        playerLivesCount = 4;
        playerLosesLives = playerHealth.playerLoseLife;
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D playerHitCollision)
    {
        if(!playerLosesLives)
        {
            if(playerHitCollision.CompareTag("EnemyBullet"))
            {
                if(playerLivesCount == 0) { Debug.Log("GAME OVER"); return; }

                playerLives[playerLivesCount-1].SetActive(false);
                playerLivesCount--;
            }

        }
    }
}