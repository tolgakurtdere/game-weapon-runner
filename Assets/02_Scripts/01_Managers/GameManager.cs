using UnityEngine;

namespace TK.Manager
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();

#if !UNITY_EDITOR && !TEST_MODE
            Debug.unityLogger.logEnabled = false;
#endif

            Application.targetFrameRate = 60;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Break();
            }
        }
#endif
    }
}