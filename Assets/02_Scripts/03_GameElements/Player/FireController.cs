using System.Collections.Generic;
using System.Linq;
using TK.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponRunner.Player
{
    public class FireController : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private WeaponBase _equippedWeapon;
        [ShowInInspector, ReadOnly] private List<WeaponBase> _weapons;
        private bool _canFire;

        private void OnEnable()
        {
            LevelManager.OnLevelStarted += OnLevelStarted;
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStarted -= OnLevelStarted;
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        private void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponBase>(true).ToList();

            _equippedWeapon = _weapons[0];
            _weapons.ForEach(w => w.Deactivate());
            _equippedWeapon.Activate();
        }

        private void OnLevelStarted()
        {
            _canFire = true;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _canFire = false;
        }

        private void LateUpdate()
        {
            if (!_canFire)
            {
                return;
            }

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
            }

            _equippedWeapon = newWeapon;
            _equippedWeapon.Activate();
        }
    }
}