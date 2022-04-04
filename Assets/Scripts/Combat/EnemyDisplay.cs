using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health health = fighter.GetTarget();
            if (health == null)
            {
                GetComponent<Text>().text = "N/A";
            }
            else if (health.GetPercentage() == 0)
            {
                GetComponent<Text>().text = "Dead";
            }
            else
            {
                GetComponent<Text>().text = String.Format("{0:0.0}%", health.GetPercentage());
            }
                
        }
    } 
}
