using System;
using System.Collections.Generic;
using UnityEngine;

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
        [Serializable]
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
        }

        [SerializeField] private Upgradeable<float> fireRate;
        [SerializeField] private Upgradeable<int> bulletDamage;
        [SerializeField] private Upgradeable<float> bulletTravelSpeed;
        [SerializeField] private Upgradeable<BulletBouncingType> bulletBouncing;
        [SerializeField] private Upgradeable<AttackFormationType> attackFormation;

        public float FireRate => fireRate.Value;
        public int BulletDamage => bulletDamage.Value;
        public float BulletTravelSpeed => bulletTravelSpeed.Value;
        public BulletBouncingType BulletBouncing => bulletBouncing.Value;
        public AttackFormationType AttackFormation => attackFormation.Value;

        public void UpgradeFireRate()
        {
            fireRate.Upgrade();
        }
    }
}