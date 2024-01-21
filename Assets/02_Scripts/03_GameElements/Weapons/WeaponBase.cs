using System;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponRunner
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField, Required] private WeaponData data;
        [SerializeField, Required] protected Bullet bulletPrefab;
        [SerializeField, Required] protected Transform muzzle;
        private float _lastFireTime;

        public WeaponData Data => data;
        private bool CanFire => Time.time >= _lastFireTime + 1 / data.FireRate;

        protected virtual void Initialize()
        {
            Debug.Log("Weapon Initialized! : " + name);
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void FireIfPossible()
        {
            if (!CanFire) return;

            Fire();
            _lastFireTime = Time.time;
        }

        protected virtual void Fire()
        {
            var bullet = LeanPool.Spawn(bulletPrefab, muzzle.position, Quaternion.identity);
            var direction = Vector3.forward;

            switch (data.AttackFormation)
            {
                case AttackFormationType.Default:
                    break;
                case AttackFormationType.Triple:
                    break;
                case AttackFormationType.TripleDouble:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            bullet.Fire(direction, data.BulletBouncing);
        }
    }
}