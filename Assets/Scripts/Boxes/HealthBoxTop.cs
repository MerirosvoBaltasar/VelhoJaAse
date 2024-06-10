using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxTop : MonoBehaviour
{
    public bool BoxOpenedFromTop;
    private Collider2D boxTopCollider;

    void Awake()
    {
        boxTopCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        BoxOpenedFromTop = false;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("PlayerFeet"))
        {
            BoxOpenedFromTop = true;
            boxTopCollider.enabled = false;
        }
    }
}
