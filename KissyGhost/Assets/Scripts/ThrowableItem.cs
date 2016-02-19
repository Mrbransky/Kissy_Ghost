﻿using UnityEngine;
using System.Collections;

public class ThrowableItem : MonoBehaviour 
{
    // Position
    public float MaxYOffset = 0.5f;
    public float MinYOffset = 1.5f;
    public float MaxXOffset = 2.5f;

    private float xDirection;

    // Time
    public float TimeToMaxY = 0.5f;
    public float ThrowDuration = 1.5f;

    private float timeSinceThrow = 0;
    private float timeToMinY;

    // Other
    private bool isInitialized = false;

    void Start()
    {
        timeToMinY = ThrowDuration - TimeToMaxY;
        this.enabled = false;
    }

    void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        Vector2 newPosition = transform.position;

        if (timeSinceThrow < ThrowDuration)
        {
            // Y position
            if (timeSinceThrow < TimeToMaxY)
            {
                newPosition.y += MaxYOffset * (Time.deltaTime / TimeToMaxY);
            }
            else
            {
                newPosition.y -= MinYOffset * (Time.deltaTime / timeToMinY);
            }

            // X position
            newPosition.x += MaxXOffset * (Time.deltaTime / ThrowDuration) * xDirection;

            transform.position = newPosition;
            timeSinceThrow += Time.deltaTime;
        }
        else
        {
            isInitialized = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            this.enabled = false;
        }
    }

    public void ThrowItem(bool isFacingRight)
    {
        if (isFacingRight)
        {
            xDirection = 1;
        }
        else
        {
            xDirection = -1;
        }

        timeSinceThrow = 0;
        GetComponent<Rigidbody2D>().isKinematic = false;
        isInitialized = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Room")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            this.enabled = false;
        }
    }
}