using System;
using TK.UI;
using UnityEngine;
using UnityEngine.UI;
using WeaponRunner.Player;

namespace WeaponRunner
{
    public class UpgradeButton : UIBase
    {
        [SerializeField] private UpgradeType upgradeType;
        public event Action OnClick;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                PlayerUpgradeController.Upgrade(upgradeType);
                OnClick?.Invoke();
            });
        }
    }
}