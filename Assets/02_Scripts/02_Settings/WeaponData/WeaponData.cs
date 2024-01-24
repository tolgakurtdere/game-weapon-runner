using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponRunner.Player;

namespace WeaponRunner
{
    public enum BulletBouncingType
    {
        None = 0,
        Twice = 10,
        Unlimited = 20
    }

    public enum AttackFormationType
    {
        Default = 0,
        Triple = 10,
        TripleDouble = 20
    }

    [CreateAssetMenu(fileName = "WeaponData", menuName = "TK/Weapon Runner/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        /*[Serializable]
        private class Upgradeable<T>
        {
            [SerializeField] private List<T> values;
            private int _upgradeIndex;

            public bool CanUpgraded => _upgradeIndex < values.Count;
            public T Value => values[_upgradeIndex];

            public void Upgrade()
            {
                if (!CanUpgraded)
                {
                    return;
                }

                _upgradeIndex++;
            }
        }*/

        [SerializeField] private List<float> fireRate;
        [SerializeField] private List<int> bulletDamage;
        [SerializeField] private List<float> bulletTravelSpeed;
        [SerializeField] private List<BulletBouncingType> bulletBouncing;
        [SerializeField] private List<AttackFormationType> attackFormation;

        public float FireRate =>
            fireRate[Mathf.Clamp(PlayerUpgradeController.Get(UpgradeType.FireRate), 0, fireRate.Count - 1)];

        public int BulletDamage =>
            bulletDamage[Mathf.Clamp(PlayerUpgradeController.Get(UpgradeType.BulletDamage), 0, bulletDamage.Count - 1)];

        public float BulletTravelSpeed =>
            bulletTravelSpeed[
                Mathf.Clamp(PlayerUpgradeController.Get(UpgradeType.BulletTravelSpeed), 0,
                    bulletTravelSpeed.Count - 1)];

        public BulletBouncingType BulletBouncing => bulletBouncing[
            Mathf.Clamp(PlayerUpgradeController.Get(UpgradeType.BulletBouncing), 0, bulletBouncing.Count - 1)];

        public AttackFormationType AttackFormation => attackFormation[
            Mathf.Clamp(PlayerUpgradeController.Get(UpgradeType.AttackFormation), 0, attackFormation.Count - 1)];
    }
}