using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpedToDeath : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerFeet"))
        {
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(Enemy);
    }

}
