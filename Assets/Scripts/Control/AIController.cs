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
        [SerializeField] PatrolPath patrolPath;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        int wayPointNum = 0;


        //Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        private void Start()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            this.transform.position = patrolPath.GetWaypoint(0);
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
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
                GuardBehavior();
            }
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        //  mover.StartMoveAction(guardPosition);
        // for (int i = 0; i < transform.childCount; i++)
        private void GuardBehavior()
        {
            Vector3 wayPoint = patrolPath.GetNextWaypoint(wayPointNum);
            mover.StartMoveAction(wayPoint);
            if (transform.position == wayPoint)
             {
                wayPointNum = patrolPath.GetNextIndex(wayPointNum);
             }
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