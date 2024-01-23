using TMPro;
using UnityEngine;

namespace WeaponRunner
{
    [RequireComponent(typeof(Collider))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        [SerializeField, Min(1)] protected int initialHp = 1;
        [SerializeField] protected TextMeshPro hpText;
        private Collider _collider;
        private int _currentHp;

        public virtual int Health
        {
            get => _currentHp;
            protected set
            {
                _currentHp = value;
                SetHpText();
            }
        }

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            Health = initialHp;
        }

        protected virtual void SetHpText()
        {
            if (hpText)
            {
                hpText.SetText(Health.ToString());
            }
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