using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        //called by Unity
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white; //procedurals API set it up then call it
                                       //alternative is having to pass in color
            Gizmos.DrawSphere(transform.position, 1f);
        }
    } 
}
