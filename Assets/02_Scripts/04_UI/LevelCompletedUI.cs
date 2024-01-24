using Sirenix.OdinInspector;
using TK.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TK.UI
{
    public class LevelCompletedUI : UIBase
    {
        [SerializeField, Required] private Button nextButton;

        private void Awake()
        {
            nextButton.onClick.AddListener(() => { LevelManager.Instance.StartNextLevel(); });
        }
    }
}