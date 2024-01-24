using System;
using System.Collections.Generic;
using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    public class PlayerUpgradeController : MonoBehaviour
    {
        private static readonly Dictionary<UpgradeType, int> UpgradeIndexDictionary = new();

        private void Awake()
        {
            foreach (UpgradeType upgradeType in Enum.GetValues(typeof(UpgradeType)))
            {
                UpgradeIndexDictionary.Add(upgradeType, 0);
            }
        }

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
            foreach (UpgradeType upgradeType in Enum.GetValues(typeof(UpgradeType)))
            {
                UpgradeIndexDictionary[upgradeType] = 0;
            }
        }

        public static int Get(UpgradeType upgradeType)
        {
            return UpgradeIndexDictionary[upgradeType];
        }

        public static void Upgrade(UpgradeType upgradeType)
        {
            UpgradeIndexDictionary[upgradeType]++;
            Debug.Log($"Upgraded! : {upgradeType}");
        }
    }
}