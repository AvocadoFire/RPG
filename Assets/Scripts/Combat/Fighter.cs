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
        [SerializeField] AudioClip audioHit;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;

        [SerializeField] Weapon defaultWeapon = null;

        AudioSource audioSource;
        Animator anim;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            anim = GetComponent<Animator>();
            //"attack" == finish attack animation (has exit time)
            //"stopAttack" = stop animation (no exit time)
            EquipWeapon(defaultWeapon);
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
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, anim);
        }
        private void AttackBehavior()
        {
            if (timeSinceLastAttack > currentWeapon.TimeBetweenAttacks())
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
            target.TakeDamage(currentWeapon.GetDamage());
            audioSource.PlayOneShot(audioHit);
        }
        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance
                (transform.position, target.transform.position) < currentWeapon.GetRange();
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

