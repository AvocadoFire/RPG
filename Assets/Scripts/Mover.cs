using System;
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
        player.speed = playerSpeed;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit 
        //Physics.Raycast()
       
    }
}
