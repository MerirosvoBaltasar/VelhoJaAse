using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform playerPosition;
    [SerializeField] private float cameraDistance;
    private Transform cameraPosition;

    private void Awake()
    {
        cameraPosition = GetComponent<Transform>();
    }
    void Start()
    {
        cameraPosition.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -cameraDistance);
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
       MoveCamera(); 
    }
    void MoveCamera()
    {
        if(playerPosition.position.y < -30)
        {
            cameraPosition.position = new Vector3(playerPosition.position.x, -40, -cameraDistance);
        }
        else
        {
            cameraPosition.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -cameraDistance); 
        }
    }
}