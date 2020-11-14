﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBlock : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Teleport(other.GetComponent<Portal>());
        }
    }

    private void Teleport(Portal portal)
    {
        Vector3 localVelocity = portal.mirrorPortalTransform.InverseTransformDirection(rigidbody.velocity);
        rigidbody.isKinematic = true;
        Vector3 localPosition = portal.transform.InverseTransformPoint(transform.position);
        transform.position = portal.mirrorPortal.transform.TransformPoint(localPosition);


        Vector3 localDirection = portal.transform.InverseTransformDirection(transform.forward);
        transform.forward = portal.mirrorPortal.transform.TransformDirection(localDirection);

        float scaleFactor =  portal.mirrorPortal.transform.localScale.x / portal.transform.localScale.x;
        transform.localScale = transform.localScale * scaleFactor;


        rigidbody.isKinematic = false;
        rigidbody.velocity = portal.mirrorPortal.transform.TransformDirection(localVelocity);
    }
}