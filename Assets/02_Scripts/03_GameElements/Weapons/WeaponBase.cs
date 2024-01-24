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
        private float _lastFireTime = float.MinValue;

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
            var defaultDirection = Vector3.forward;
            var bulletParameters = new Bullet.BulletParameters
            {
                Damage = data.BulletDamage,
                TravelSpeed = data.BulletTravelSpeed,
                BouncingType = data.BulletBouncing
            };

            switch (data.AttackFormation)
            {
                case AttackFormationType.Default:
                    Fire(defaultDirection, bulletParameters);
                    break;
                case AttackFormationType.Triple:
                    Fire(defaultDirection, bulletParameters);
                    Fire(Quaternion.Euler(0, 30, 0) * defaultDirection, bulletParameters);
                    Fire(Quaternion.Euler(0, -30, 0) * defaultDirection, bulletParameters);
                    break;
                case AttackFormationType.TripleDouble:
                    Fire(defaultDirection, bulletParameters, 0.05f);
                    Fire(defaultDirection, bulletParameters, -0.05f);
                    Fire(Quaternion.Euler(0, 30, 0) * defaultDirection, bulletParameters);
                    Fire(Quaternion.Euler(0, -30, 0) * defaultDirection, bulletParameters);
                    Fire(Quaternion.Euler(0, 20, 0) * defaultDirection, bulletParameters);
                    Fire(Quaternion.Euler(0, -20, 0) * defaultDirection, bulletParameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Fire(Vector3 direction, Bullet.BulletParameters parameters, float muzzleOffset = 0f)
        {
            var position = muzzle.position;
            position.x += muzzleOffset;
            var bullet = LeanPool.Spawn(bulletPrefab, position, Quaternion.identity);
            bullet.Fire(direction, parameters);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(muzzle.position, 0.02f);
        }
    }
}