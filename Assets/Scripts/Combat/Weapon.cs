using RPG.Core;
using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;


        public void Spawn(Transform righthand, Transform lefthand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(righthand, lefthand);
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
            animator.runtimeAnimatorController = animatorOverride;
            }

        }

        private Transform GetTransform(Transform righthand, Transform lefthand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = righthand;
            else handTransform = lefthand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, 
                GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return (weaponRange);
        }

        internal float TimeBetweenAttacks()
        {
            return timeBetweenAttacks;
        }
    }
}