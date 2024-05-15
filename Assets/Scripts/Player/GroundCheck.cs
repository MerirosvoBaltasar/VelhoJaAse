using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //Making the isGrounded-variable public so that it can be accessed by other scripts.
    public bool isGrounded;

    //The groundCheckPoint is a player gamobject's child at its feet. From this location
    //it will be checked if the parent gameobject is grounded.
    [SerializeField] Transform groundCheckPoint;
    private LayerMask groundLayer;
    [SerializeField] float checkRadius;

    //Initialize the groundLayer layermask.
    void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Start()
    {
        
    }

    //With each frame check if the gamobject is within a certain distance, defined by checkRadius,
    //from the groundLayer.
    public void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer);
    }
}
