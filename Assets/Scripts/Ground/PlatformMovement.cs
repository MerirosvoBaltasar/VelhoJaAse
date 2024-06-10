using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform platformPosition;
    //How long it takes for the platform to go up and down.
    [SerializeField] private float moveSpeed;
    private bool platformDown;
    [SerializeField] private float movementHeight;
    private Vector3 platformDownPosition;
    private Vector3 platformUpPosition;

    void Start()
    {
        platformDown = false;
        platformDownPosition = transform.position + new Vector3(0, -movementHeight, 0);
        platformUpPosition = transform.position + new Vector3(0, movementHeight, 0);
    }
    void FixedUpdate()
    {
        PlatformDisplacement();
    }

    void PlatformDisplacement()
    {
        if(platformDown)
        {
            RaisePlatform();
            platformDown = transform.position.y < platformUpPosition.y;
        }
        if(!platformDown)
        {
            LowerPlatform();
            platformDown = transform.position.y < platformDownPosition.y;
        }
    }
    void RaisePlatform()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
    void LowerPlatform()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
