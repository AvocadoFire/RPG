using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] GameObject transformTarget;
    [SerializeField] float playerSpeed = 5f;
    NavMeshAgent player;
    Ray lastRay;


    void Start()
    {
       player =  GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        player.speed = playerSpeed;
        player.destination = transformTarget.transform.position;
    }
}
