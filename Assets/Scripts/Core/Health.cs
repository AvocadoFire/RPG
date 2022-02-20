using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
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

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            if (healthPoints > Mathf.Epsilon)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                print(healthPoints);
            }
            if (healthPoints <= Mathf.Epsilon)
            {
                Die();
            }
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
