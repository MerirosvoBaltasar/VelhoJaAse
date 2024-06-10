using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] private Transform spikePosition;
    //How long it takes for the spikes to go up and down.
    [SerializeField] private float movementDuration;
    [SerializeField] private GameObject Player;
    private Rigidbody2D playerBody;
    //Boolean to check whether the spikes are up or down, that is, whether they should descend or ascend.
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
        //Initial position of the spikes.
        Vector3 startingPosition = spikePosition.position;
        //If spikes are down, move them up, if down, make them rise.
        Vector3 destination = spikesDown? spikesUpPosition : spikesDownPosition;
        //Start a coroutine to make the spikes rise slowly.
        StartCoroutine(MoveSpikes(startingPosition, destination));
    }

    IEnumerator MoveSpikes(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0;
        //Interpolate spike position slowly, during the prescribed time.
        while(elapsedTime < movementDuration)
        {
            spikePosition.position = Vector3.Lerp(start, end, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    //If player hits the spikes, make him jump a bit.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerBody.AddForce(Vector2.up*3.0f, ForceMode2D.Impulse);
        }
    }
}
