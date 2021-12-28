using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] GameObject transformTarget;
    [SerializeField] float playerSpeed = 5f;
    NavMeshAgent player;

    void Start()
    {
       player =  GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        player.speed = playerSpeed;
        player.destination = transformTarget.transform.position;
    }
}
