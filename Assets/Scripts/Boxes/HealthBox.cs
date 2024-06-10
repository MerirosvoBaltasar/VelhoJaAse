using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBox : MonoBehaviour
{
    private bool BoxOpen;
    SpriteRenderer boxSprite;
    [SerializeField] Sprite healthItemSprite;
    [SerializeField] float animationWaitTime;
    [SerializeField] private GameObject boxTop;
    [SerializeField] GameObject Player;
    private PlayerShoots shootingScript;
    private BoxCollider2D healthBoxCollider;
    private CapsuleCollider2D triggerCollider;
    private HealthBoxTop boxTopScript;
    private bool boxOpened;

    void Start()
    {
        boxSprite = GetComponent<SpriteRenderer>();
        healthBoxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<CapsuleCollider2D>();
        boxTopScript = boxTop.GetComponent<HealthBoxTop>();
        shootingScript = Player.GetComponent<PlayerShoots>();
        boxOpened = false;
    }

    void Update()
    {
        if(!boxOpened && boxTopScript.BoxOpenedFromTop)
        {
            StartCoroutine(ChangeBoxSprite());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!boxOpened && col.CompareTag("PlayerBullet"))
        {
            StartCoroutine(ChangeBoxSprite());
        }

        if(boxOpened && col.CompareTag("Player"))
        {
            shootingScript.ammo += 50;
            Destroy(gameObject);
        }
    }
    IEnumerator ChangeBoxSprite()
    {
        yield return new WaitForSeconds(animationWaitTime);
        boxOpened = true;
        boxSprite.sprite = healthItemSprite;
        healthBoxCollider.size = new Vector2(1.0f, 1.5625f);
        triggerCollider.size = new Vector2(1.1f, 1.6f);
        gameObject.tag = "HealthItem";
    }
}
