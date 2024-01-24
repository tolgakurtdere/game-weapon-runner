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
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStarted += OnLevelStarted;
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
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

        // LateUpdate because fire action should be happened after animation changes
        private void LateUpdate()
        {
            if (!_canFire)
            {
                return;
            }

            _equippedWeapon.FireIfPossible();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Gate gate))
            {
                gate.gameObject.SetActive(false);
                ChangeWeapon(gate.WeaponData);
            }
        }

        private void OnLevelLoaded(int levelNo)
        {
            _equippedWeapon.Deactivate();
            _equippedWeapon = _weapons[0];
            _equippedWeapon.Activate();
        }

        private void OnLevelStarted(int levelNo)
        {
            _canFire = true;
        }

        private void OnLevelStopped(int levelNo, bool isSuccess)
        {
            _canFire = false;
        }

        private void ChangeWeapon(WeaponData weaponData)
        {
            var newWeapon = _weapons.FirstOrDefault(w => w.Data == weaponData);
            if (!newWeapon)
            {
                Debug.LogError($"WeaponData could not get found! : {weaponData}");
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