using Sirenix.OdinInspector;
using TK.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TK.UI
{
    public class LevelFailedUI : UIBase
    {
        [SerializeField, Required] private Button retryButton;

        private void Awake()
        {
            retryButton.onClick.AddListener(() => { LevelManager.Instance.RestartLevel(); });
        }
    }
}