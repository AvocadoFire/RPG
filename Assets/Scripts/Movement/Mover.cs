using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        Ray lastRay;
        float speed;
        NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }
        void UpdateAnimator()
        {
            var vel = navMeshAgent.velocity;
            var localVelocity = transform.InverseTransformDirection(vel);
            float speed = localVelocity.z;
            GetComponentInChildren<Animator>().SetFloat("forwardSpeed", speed);
        }
    }

}