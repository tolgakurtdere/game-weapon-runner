using UnityEngine;

namespace WeaponRunner.Damageable
{
    [RequireComponent(typeof(Collider))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        [SerializeField, Min(1)] protected int initialHealth = 1;
        private Collider _collider;

        public virtual int Health { get; protected set; }

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            Health = initialHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            _collider.enabled = false;
        }
    }
}