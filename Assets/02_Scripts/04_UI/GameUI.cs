using TMPro;
using UnityEngine;

namespace TK.UI
{
    public class GameUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI levelText;

        public void SetLevelText(int levelNo)
        {
            levelText.text = $"Level {levelNo}";
        }
    }
}