﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public FPS_CharacterController fps;
    public float rayLength;
    public bool isGrabbed;
    private Rigidbody grabbedObject;
    public GameObject shootingPoint;
    private LineRenderer laser;
    public bool active;
    private int damage = 250;
    public bool broken;
    private void Start()
    {
        broken = true;
        active = true;
        isGrabbed = false;
        laser = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (broken)
        {
            Vector3 fwd = shootingPoint.transform.forward;
            Ray ray = new Ray(shootingPoint.transform.position, fwd);
            RaycastHit rayCastHit;
            if (active)
            {
                Debug.DrawRay(shootingPoint.transform.position, fwd * rayLength, Color.green);
                Physics.Raycast(shootingPoint.transform.position, fwd);
                laser.SetPosition(0, shootingPoint.transform.position);
                laser.SetPosition(1, fwd * rayLength + shootingPoint.transform.position);
                if (Physics.Raycast(ray, out rayCastHit, rayLength))
                {
                    print(rayCastHit.collider.gameObject.tag.ToString());

                    if (rayCastHit.collider.gameObject.name.Equals("Capsule"))
                    {
                        fps.LoseHeal(damage);
                    }
                    if (rayCastHit.collider.gameObject.CompareTag("CompanionCube"))
                    {
                        active = false;
                    }
                }

            }
            else
            {
                if (Physics.Raycast(ray, out rayCastHit, rayLength))
                {
                    laser.SetPosition(0, shootingPoint.transform.position);
                    laser.SetPosition(1, rayCastHit.transform.position);
                    if (rayCastHit.collider.gameObject.tag != "CompanionCube")
                    {
                        active = true;
                    }

                }
            }
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Turret"))
        {
            broken = false;
        }
        else if (other.gameObject.tag.Equals("CompanionCube"))
        {
            broken = false;
        }
    }

}
