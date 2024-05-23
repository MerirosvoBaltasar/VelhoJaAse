using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{

    [SerializeField] private Transform playerPosition;
    private Transform cameraPosition;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] float velocityToBackUpCamera;
    [SerializeField] private float interpolationTotalFrames;
    private float elapsedFrames;
    private Vector3 cameraDistanceDifference;
    private float cameraCurrentDistance;
    Vector3 startingPosition;

    private void Awake()
    {
        cameraPosition = GetComponent<Transform>();
        cameraDistanceDifference = new Vector3(0, 0, 5);
    }
    void Start()
    {
        elapsedFrames = 0;
        cameraPosition.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -10);
    }

    void Update()
    {
    }

    private void LateUpdate()
    {
        startingPosition = cameraPosition.position;

        if(playerBody.velocity.y > velocityToBackUpCamera)
        {
            if (cameraPosition.position.z > -15)
            { PullCameraBack(); }
        }
        if(playerBody.velocity.y < - 2)
        {
            if (cameraPosition.position.z < -10)
            { PushCameraCloser(); }
        }
        else
        {
            cameraPosition.position = new Vector3(playerPosition.position.x, playerPosition.position.y, cameraCurrentDistance); 
            elapsedFrames = 0;
        }
    }

    void PullCameraBack()
    {
        float interpolationRatio = elapsedFrames / interpolationTotalFrames;
        cameraPosition.position = Vector3.Lerp(startingPosition, startingPosition - cameraDistanceDifference, interpolationRatio);
        elapsedFrames++;
    }

    void PushCameraCloser()
    {
        float interpolationRatio = elapsedFrames / interpolationTotalFrames;
        cameraPosition.position = Vector3.Lerp(startingPosition, startingPosition + cameraDistanceDifference, interpolationRatio);
        elapsedFrames++;
    }
}
