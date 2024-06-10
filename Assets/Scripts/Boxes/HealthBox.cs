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
        //Get references to variables and scripts needed.
        boxSprite = GetComponent<SpriteRenderer>();
        healthBoxCollider = GetComponent<BoxCollider2D>();
        triggerCollider = GetComponent<CapsuleCollider2D>();
        boxTopScript = boxTop.GetComponent<HealthBoxTop>();
        shootingScript = Player.GetComponent<PlayerShoots>();
        boxOpened = false;
    }

    void Update()
    {
        //If the box is not opened but its top is opened (that is, if the player jumps on the box), change sprite from box to health item.
        if(!boxOpened && boxTopScript.BoxOpenedFromTop)
        {
            StartCoroutine(ChangeBoxSprite());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the box is unopened and is hit by player's bullet, open the box.
        if(!boxOpened && col.CompareTag("PlayerBullet"))
        {
            StartCoroutine(ChangeBoxSprite());
        }

        //If the box is open and player touches it, add 50 points to player's ammo, then destroy gameobject.
        if(boxOpened && col.CompareTag("Player"))
        {
            shootingScript.ammo += 50;
            Destroy(gameObject);
        }

        //If the box is open and player's feet touch it, add 50 points to player's ammo, then destroy gameobject.
        if(boxOpened && col.CompareTag("PlayerFeet"))
        {
            shootingScript.ammo += 50;
            Destroy(gameObject);
        }
    }
    IEnumerator ChangeBoxSprite()
    {
        //This is a coroutine to open the box slowly, thus giving time to operate the animation.
        yield return new WaitForSeconds(animationWaitTime);
        boxOpened = true;
        boxSprite.sprite = healthItemSprite;
        healthBoxCollider.size = new Vector2(1.0f, 1.5625f);
        triggerCollider.size = new Vector2(1.1f, 1.6f);
        gameObject.tag = "HealthItem";
    }
}
