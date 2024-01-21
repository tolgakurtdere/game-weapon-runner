using System.Collections;
using Lean.Pool;
using UnityEngine;

namespace WeaponRunner
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Min(0)] private int damage = 1;
        private Rigidbody _rigidbody;
        private BulletBouncingType _bouncingType;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                // Deactivate();
            }
        }

        public void Fire(Vector3 direction, BulletBouncingType bouncingType)
        {
            _bouncingType = bouncingType;
            transform.rotation = Quaternion.LookRotation(direction);
            _rigidbody.AddForce(direction * 1000);
            Deactivate(0.7f);
        }

        private void Deactivate(float delay)
        {
            if (delay > 0)
            {
                StartCoroutine(DeactivateWithDelay(delay));
            }
            else
            {
                LeanPool.Despawn(this);
            }
        }

        private IEnumerator DeactivateWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            Deactivate(0f);
        }
    }
}