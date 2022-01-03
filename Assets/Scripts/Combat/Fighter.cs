using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
      

        private void Update()
        {
            if (target!= null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                if (transform.position.z - target.position.z < weaponRange)
                { 
                    GetComponent<Mover>().Stop();
                }
            }
            
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("OMG So cute!!");
        }
    }
}

