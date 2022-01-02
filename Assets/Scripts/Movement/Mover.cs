using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    Ray lastRay;
    float speed;
    void Update()
    {

        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    void UpdateAnimator()
    {
        var vel = GetComponent<NavMeshAgent>().velocity;
        var localVelocity = transform.InverseTransformDirection(vel);
        float speed = localVelocity.z;
        GetComponentInChildren<Animator>().SetFloat("forwardSpeed", speed);
    }
}
