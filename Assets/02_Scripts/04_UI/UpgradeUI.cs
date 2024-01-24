using System.Collections.Generic;
using TK.Manager;
using TK.UI;
using UnityEngine;

namespace WeaponRunner
{
    public class UpgradeUI : UIBase
    {
        private UpgradeButton[] _upgradeButtons;

        private void Awake()
        {
            _upgradeButtons = GetComponentsInChildren<UpgradeButton>(true);
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.OnClick += Hide;
            }
        }

        public override void Show()
        {
            base.Show();
            InitializeButtons();
            GameManager.TimeScale = 0f;
        }

        public override void Hide()
        {
            base.Hide();
            GameManager.TimeScale = 1f;
        }

        private void InitializeButtons()
        {
            var length = _upgradeButtons.Length;
            var indexList = new List<int>(length);
            for (var i = 0; i < length; i++)
            {
                indexList.Add(i);
            }

            // hide all upgrade buttons
            foreach (var upgradeButton in _upgradeButtons)
            {
                upgradeButton.Hide();
            }

            // select 3 upgrade button randomly, and show them
            for (var i = 0; i < 3; i++)
            {
                var index = Random.Range(0, indexList.Count);
                _upgradeButtons[indexList[index]].Show();
                indexList.RemoveAt(index);
            }
        }
    }
}