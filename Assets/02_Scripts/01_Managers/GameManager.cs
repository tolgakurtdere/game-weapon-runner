using Sirenix.OdinInspector;
using UnityEngine;

namespace TK.Manager
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField, Required] private Joystick joystick;

        public static Joystick Joystick => Instance.joystick;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.S)) Time.timeScale = 1;
        }
#endif
    }
}