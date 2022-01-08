using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 20f;
        Animator anim;
        public bool isDead = false;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            if (healthPoints > Mathf.Epsilon)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                print(healthPoints); 
            }
            if (!isDead && healthPoints <= Mathf.Epsilon)
            {
                anim.SetBool("die", true);
                isDead = true;
            }
        }
    }
}
