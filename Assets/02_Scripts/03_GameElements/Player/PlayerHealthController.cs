using System;
using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerHealthController : MonoBehaviour
    {
        public static event Action OnPlayerDied;
        private Collider _collider;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                Die();
            }
        }

        private void OnLevelLoaded()
        {
            _collider.enabled = true;
        }

        private void Die()
        {
            _collider.enabled = false;

            LevelManager.StopLevel(false);

            OnPlayerDied?.Invoke();
        }
    }
}