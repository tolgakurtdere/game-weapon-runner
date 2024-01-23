using System;
using Lean.Pool;
using TK.Utility;
using UnityEngine;

namespace WeaponRunner
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TimerBehaviour))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        public struct BulletParameters
        {
            public int Damage;
            public float TravelSpeed;
            public BulletBouncingType BouncingType;
        }

        private const int FIRE_FORCE_MULTIPLIER = 1000;
        private const float LIFETIME = 1f;
        private Rigidbody _rigidbody;
        private TimerBehaviour _timerBehaviour;
        private int _bounceCounter;
        private BulletParameters Parameters { get; set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _timerBehaviour = GetComponent<TimerBehaviour>();
        }

        private void OnCollisionEnter(Collision other)
        {
            _bounceCounter++;
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Parameters.Damage);
            }

            switch (Parameters.BouncingType)
            {
                case BulletBouncingType.None:
                    Deactivate();
                    break;
                case BulletBouncingType.Twice:
                    if (_bounceCounter == 3) Deactivate();
                    break;
                case BulletBouncingType.Unlimited:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Fire(Vector3 direction, BulletParameters parameters)
        {
            Parameters = parameters;
            transform.rotation = Quaternion.LookRotation(direction);
            _rigidbody.AddForce(direction * (FIRE_FORCE_MULTIPLIER * parameters.TravelSpeed));
            Deactivate(LIFETIME);
        }

        private void Deactivate(float delay = 0f)
        {
            if (delay > 0)
            {
                _timerBehaviour.StartTimer(delay, () => { Deactivate(); });
            }
            else
            {
                LeanPool.Despawn(this);
            }
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _timerBehaviour.StopTimer();
            _bounceCounter = 0;
            Parameters = new BulletParameters();
        }
    }
}