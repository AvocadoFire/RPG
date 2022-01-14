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
            const float waypointGizmoRadius = .3f;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j;
                if (i == 0) { Gizmos.color = Color.red; }
                else if (i == 1) { Gizmos.color = Color.yellow; }
                else Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                j = GetNextIndex(i);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            //should use modular arithmetic here
            int j;
            if (i == transform.childCount - 1) j = 0;
            else j = i + 1;
            return j;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).transform.position;
        }

        public Vector3 GetNextWaypoint(int i)
        {
            int j = GetNextIndex(i);
            return transform.GetChild(j).transform.position;
        }
    } 
}
