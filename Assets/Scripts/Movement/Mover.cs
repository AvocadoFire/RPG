using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    Animator anim;
    NavMeshAgent playerNav;
    Ray lastRay;
    float speed;
    void Start()
    {
       playerNav =  GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
         if (hasHit)
        {
            MoveTo(hit);
        }
    }

    private void MoveTo(RaycastHit hit)
    {
        playerNav.destination = hit.point;
    }

    void UpdateAnimator()
    {
        var vel = playerNav.velocity;
        var localVelocity = transform.InverseTransformDirection(vel);
        float speed = localVelocity.z;
        anim.SetFloat("forwardSpeed", speed);
    }
}
