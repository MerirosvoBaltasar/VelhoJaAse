using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Health playerHealthScript;
    private Collider2D groundCollider;

    void Awake()
    {
        groundCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //If the player is dead, disable ground collider so that the player falls through the ground.
        playerHealthScript = Player.GetComponent<Health>();
        if(playerHealthScript.playerDead)
        {
            groundCollider.enabled = false;
        } 
    }
}
