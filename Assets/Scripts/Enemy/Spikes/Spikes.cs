using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private Transform spikePosition;
    [SerializeField] private float movementDuration;
    [SerializeField] private GameObject Player;
    private Rigidbody2D playerBody;
    private bool spikesDown;
    private Vector3 spikesDownPosition;
    private Vector3 spikesUpPosition;

    void Start()
    {
        spikesDown = false;
        playerBody = Player.GetComponent<Rigidbody2D>();
        spikesDownPosition = transform.position;
        spikesUpPosition = transform.position + new Vector3(0, 0.6f, 0);
        InvokeRepeating("SpikeMovement", 3.0f, 2.0f);
    }

    void Update()
    {
        
    }

    void SpikeMovement()
    {
        spikesDown = !spikesDown;
        Vector3 startingPosition = spikePosition.position;
        Vector3 destination = spikesDown? spikesUpPosition : spikesDownPosition;

        StartCoroutine(MoveSpikes(startingPosition, destination));
    }

    IEnumerator MoveSpikes(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0;

        while(elapsedTime < movementDuration)
        {
            spikePosition.position = Vector3.Lerp(start, end, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerBody.AddForce(Vector2.up*3.0f, ForceMode2D.Impulse);
        }
    }
}