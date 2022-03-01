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
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] AudioClip audioHit;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;

        AudioSource audioSource;
        Animator anim;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            anim = GetComponent<Animator>();
            //"attack" == finish attack animation (has exit time)
            //"stopAttack" = stop animation (no exit time)
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead())
            {
                StopAttack();
                return;
            }

            if (!GetIsInRange())
            {
                transform.LookAt(target.transform.position);
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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
                transform.LookAt(target.transform.position);

                //This will trigger the Hit() event
                TriggerAtack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAtack()
        {
            anim.ResetTrigger("stopAttack");
            anim.SetTrigger("attack");
        }

        //animation event
        void Hit()
        {
            if (target == null ) 
            {
                StopAttack();
               return; 
            }
            target.TakeDamage(weaponDamage);
            audioSource.PlayOneShot(audioHit);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance
                (transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health targetHealth = GetComponent<Health>();
            if (combatTarget == null) { print("combat target null");  return false; }
            if (targetHealth.IsDead()) { print("target dead"); return false; }
            return true;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("stopAttack");
        }
    }
}

