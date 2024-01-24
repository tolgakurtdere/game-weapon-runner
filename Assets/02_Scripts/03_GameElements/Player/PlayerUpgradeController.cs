using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    public class PlayerUpgradeController : MonoBehaviour
    {
        public static int FireRateUpgradeIndex { get; private set; }
        public static int BulletDamageUpgradeIndex { get; private set; }
        public static int BulletTravelSpeedUpgradeIndex { get; private set; }
        public static int BulletBouncingUpgradeIndex { get; private set; }
        public static int AttackFormationUpgradeIndex { get; private set; }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(int levelNo)
        {
            FireRateUpgradeIndex = 0;
            BulletDamageUpgradeIndex = 0;
            BulletTravelSpeedUpgradeIndex = 0;
            BulletBouncingUpgradeIndex = 0;
            AttackFormationUpgradeIndex = 0;
        }
    }
}