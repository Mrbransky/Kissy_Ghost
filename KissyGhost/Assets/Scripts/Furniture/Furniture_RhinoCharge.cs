﻿using UnityEngine;
using System.Collections;

public class Furniture_RhinoCharge : MonoBehaviour
{
    [SerializeField]
    private float minFollowDistance = 0.1f;
    [SerializeField]
    private float followSpeed = 3.5f;
    public int BounceBackForce;

    private KissableFurniture _KissableFurniture;
    private Transform closestPlayerTransform;
    private Rigidbody2D furnitureRigidbody2D;
    private bool isInitialized = false;
    private Vector3 lastKnownPlayerPosition;
    void Start()
    {
        _KissableFurniture = GetComponent<KissableFurniture>();
        furnitureRigidbody2D = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    void Update()
    {
        if (!isInitialized)
        {
            return;
        }
        else if (closestPlayerTransform == null)
        {
            _KissableFurniture.UnkissFurniture();
            return;
        }
        //Vector2 moveDir = closestPlayerTransform.position - transform.position;
        //moveDir.Normalize();
        //float distanceFromPlayer = moveDir.magnitude;
        Debug.Log(lastKnownPlayerPosition);
        furnitureRigidbody2D.velocity = (lastKnownPlayerPosition - transform.position) * followSpeed;

        //if (distanceFromPlayer > minFollowDistance)
        //{
        //    furnitureRigidbody2D.velocity = moveDir * followSpeed;
        //}
        //else
        //{
        //    furnitureRigidbody2D.velocity = Vector2.zero;
        //}
    }

    void OnDisable()
    {
        if (isInitialized)
        {
            isInitialized = false;
            furnitureRigidbody2D.velocity = Vector2.zero;

            Vector3 furnitureToPlayerDir = closestPlayerTransform.position - transform.position;
            float distanceToPlayer = furnitureToPlayerDir.magnitude;

            if (distanceToPlayer <= minFollowDistance)
            {
                closestPlayerTransform.GetComponent<Rigidbody2D>().AddForce(furnitureToPlayerDir.normalized * BounceBackForce);
            }
        }
    }




    public void Initialize(Transform _closestPlayerTransform)
    {
        bool temp = true;
        closestPlayerTransform = _closestPlayerTransform;

        if(temp)
        {
            lastKnownPlayerPosition = _closestPlayerTransform.position;
            temp = false;
        }
        isInitialized = true;
    }
}