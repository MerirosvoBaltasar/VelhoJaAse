using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemyDamage;
    private Rigidbody2D enemyBody;
    [SerializeField] private float deathDelay;
    private EnemyMovement enemyMovement;
    private EnemyShoot enemyShoot;
    private bool enemyDead;
    [SerializeField] private float rotationIncrement;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyShoot = GetComponent<EnemyShoot>();
    }

    void Update()
    {
        if(enemyDead)
        {
            RotateDeadEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D enemyHitCollision)
    {
        if(enemyHitCollision.CompareTag("PlayerBullet"))
        {
            enemyHealth -= enemyDamage;

            if(enemyHealth <= 0)
            {
                enemyDead = true;
                enemyMovement.enabled = false;
                enemyShoot.enabled = false;
            }
        }
    }

    void RotateDeadEnemy()
    {
        Vector3 incrementVector = new Vector3(transform.right.y, transform.right.x, 0) * rotationIncrement * Time.deltaTime;
        Vector3 rotationVector = transform.right + incrementVector;
        float roundingFactor = Mathf.Abs(transform.right.x);

        if(roundingFactor > 0.05)
        {
            enemyBody.freezeRotation = false;
            transform.rotation *= Quaternion.FromToRotation(transform.right, rotationVector);
        }
        else
        {
            StartCoroutine(EnemyDeath());
            enemyBody.freezeRotation = true;

        }
    }
    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
