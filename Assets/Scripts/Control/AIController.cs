using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        int currentWaypointIndex = 0;
        //Vector3 guardPosition;
        float timeSinceAtWaypoint = Mathf.Infinity;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();

            this.transform.position = patrolPath.GetWaypoint(0);
        }

        private void Update()
        {
            timeSinceAtWaypoint += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;

            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehavior();
                timeSinceLastSawPlayer = 0;
            }

            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void PatrolBehavior()
        {
            if (patrolPath == null) return;

            MoveToNextWaypoint();

            if (CheckAtWaypoint())
            {
                timeSinceAtWaypoint = 0;
                CycleWaypoint();
            }
        }

        private void MoveToNextWaypoint()
        {
            if (timeSinceAtWaypoint > waypointDwellTime)
            {
            mover.StartMoveAction(patrolPath.GetNextWaypoint(currentWaypointIndex), patrolSpeedFraction);
            }
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool CheckAtWaypoint()
        {
            var nextWaypoint = patrolPath.GetNextWaypoint(currentWaypointIndex);
            float distanceToWaypoint = Vector3.Distance(transform.position, nextWaypoint);
            return distanceToWaypoint < waypointTolerance;
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //called by Unity
           private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue; //procedurals API set it up then call it
                                       //alternative is having to pass in color
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}