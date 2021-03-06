using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 20f;
        [SerializeField] AudioClip audioKill;
        AudioSource audioSource;
        Animator anim;
        bool isDead = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            healthPoints = GetComponent <BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (healthPoints > Mathf.Epsilon)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                print(healthPoints);
            }
            if (healthPoints <= Mathf.Epsilon)
            {
                Die();
                AwardExperience(instigator);
            }
        }



        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (isDead) return;

            anim.SetTrigger("die");
           // anim.SetBool("die", true);
            isDead = true;
            audioSource.PlayOneShot(audioKill);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}
