using TK.UI;
using UnityEngine;
using WeaponRunner;

namespace TK.Manager
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [SerializeField] private HomeUI homeUI;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private LevelCompletedUI levelCompletedUI;
        [SerializeField] private LevelFailedUI levelFailedUI;
        [SerializeField] private UpgradeUI upgradeUI;

        public UpgradeUI UpgradeUI => upgradeUI;

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

        private void OnLevelLoaded(int levelNo)
        {
            levelCompletedUI.Hide();
            levelFailedUI.Hide();

            gameUI.SetLevelText(levelNo);
            gameUI.Show();
            homeUI.Show();
        }

        private void OnLevelStarted(int levelNo)
        {
            homeUI.Hide();
        }

        private void OnLevelStopped(int levelNo, bool isSuccess)
        {
            gameUI.Show();
            if (isSuccess)
            {
                levelCompletedUI.Show();
            }
            else
            {
                levelFailedUI.Show();
            }
        }
    }
}