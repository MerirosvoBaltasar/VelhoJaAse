using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Transform rockTrigger;
    [SerializeField] private GameObject rockSupport;
    [SerializeField] private Transform playerPosition;
    private bool freeRock;
    private bool rockReleased;

      void Start()
    {
        freeRock = false;
        rockReleased = false;
    }

    void Update()
    {
        if (!rockReleased) { RealeaseRockIfTriggered(); }
        if (transform.position.y < -30) { Destroy(gameObject); }
        
    }

    void RealeaseRockIfTriggered()
    {
        freeRock = playerPosition.position.x > rockTrigger.position.x;
        if(freeRock) { Destroy(rockSupport);  rockReleased = true; }
    }
}
