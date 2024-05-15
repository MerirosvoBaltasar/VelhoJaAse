using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform playerPosition;
    private Transform cameraPosition;

    private void Awake()
    {
        cameraPosition = GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        cameraPosition.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -10);
    }
}
