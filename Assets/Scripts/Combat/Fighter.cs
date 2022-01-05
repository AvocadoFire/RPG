using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Animator anim;

        Transform target;
        float timeSinceLastAttack = 3f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            
            if (!GetIsInRange())
           {
                GetComponent<Mover>().MoveTo(target.position);
           }
           else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                anim.SetTrigger("attack");
                Debug.Log("attack here");
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            print("OMG So cute!!");
        }
        
        public void Cancel()
        {
            target = null;
            anim.ResetTrigger("attack");
        }

        //animation event (not in mine though)
        void Hit()
        {
            return;
        }

    }
}

