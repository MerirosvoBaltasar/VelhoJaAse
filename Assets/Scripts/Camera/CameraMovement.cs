using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform bossSceneThreshold;
    [SerializeField] private float cameraDistance;
    private bool playerEnteredBossScene;
    private Transform cameraPosition;

    private void Awake()
    {
        cameraPosition = GetComponent<Transform>();
    }
    void Start()
    {
        playerEnteredBossScene = false;
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
        EnterBossScene();

        if(playerPosition.position.y < -40)
        {
            cameraPosition.position = new Vector3(playerPosition.position.x, -40, -cameraDistance);
        }
        else if (!playerEnteredBossScene)
        {
            StartCoroutine(CameraMovementDelay(playerPosition.position.x));
        }
        else
        {
            if(playerPosition.position.x < bossSceneThreshold.position.x)
            {
                StartCoroutine(CameraMovementDelay(playerPosition.position.x));
            }
            else
            {
                StartCoroutine(CameraMovementDelay(bossSceneThreshold.position.x));
            }
        }
    }

    private bool EnterBossScene()
    {
        if (playerPosition.position.x < bossSceneThreshold.position.x)
        {
            playerEnteredBossScene = true;    
        }
        return playerEnteredBossScene;
    }

    IEnumerator CameraMovementDelay(float xPosition)
    {
        for(int i = 0; i < 3; i++)
        {
            yield return null;
        }
        cameraPosition.position = new Vector3(xPosition, playerPosition.position.y, -cameraDistance); 
    }
}