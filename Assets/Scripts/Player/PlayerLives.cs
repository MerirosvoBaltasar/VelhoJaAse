using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] List<GameObject> playerLives = new List<GameObject>();
    private int playerLivesCount;
    private int playerLivesUpdate;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();

    }
    void Start()
    {
        playerLivesUpdate = playerHealth.playerLives;
        playerLivesCount = playerLivesUpdate;
        
    }

    void Update()
    {
        if(playerLivesCount > 0)
        {
            playerLivesUpdate = playerHealth.playerLives;

            if(playerLivesCount > playerLivesUpdate)
            {
                playerLives[playerLivesCount-1].SetActive(false);
                playerLivesCount--;
                Debug.Log(playerLivesCount);
            }
        } 
    }
}