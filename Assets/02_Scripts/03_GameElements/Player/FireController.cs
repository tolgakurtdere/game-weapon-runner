using System.Collections.Generic;
using System.Linq;
using TK.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponRunner.Player
{
    [RequireComponent(typeof(PlayerWrapper))]
    public class FireController : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private WeaponBase _equippedWeapon;
        [ShowInInspector, ReadOnly] private List<WeaponBase> _weapons;
        private Animator _animator;
        private PlayerWrapper _playerWrapper;

        private void OnEnable()
        {
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        private void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponBase>(true).ToList();
            _playerWrapper = GetComponent<PlayerWrapper>();

            _animator = _playerWrapper.AnimatorController.Animator;
            _equippedWeapon = _weapons[0];

            _weapons.ForEach(w => w.Deactivate());
            _equippedWeapon.Activate();
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _animator.SetBool(AnimatorParameters.Bools.IsFiring, false);
        }

        private void Update()
        {
            _equippedWeapon.FireIfPossible();
        }

        private void OnWeaponSelected(WeaponData selectedWeapon)
        {
            var newWeapon = _weapons.FirstOrDefault(w => w.Data == selectedWeapon);
            if (!newWeapon)
            {
                Debug.LogError("WeaponData could not get found! : " + selectedWeapon);
                return;
            }

            if (_equippedWeapon)
            {
                _equippedWeapon.Deactivate();
                // _animator.SetBool(_equippedWeapon.Data.AnimationParameter, false);
            }

            _equippedWeapon = newWeapon;
            _equippedWeapon.Activate();
            // _animator.SetBool(_equippedWeapon.Data.AnimationParameter, true);
        }
    }
}