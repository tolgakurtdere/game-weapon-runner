using System;
using System.Collections.Generic;
using System.Linq;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class LevelManager : SingletonBehaviour<LevelManager>
    {
        public static event Action OnLevelLoaded;
        public static event Action OnLevelStarted;
        public static event Action<bool> OnLevelStopped;

        [SerializeField] private List<Level> levelPrefabs = new();
        private CyclingList<Level> _levels;

        public static bool IsPlaying { get; set; }
        public static Level CurrentLevel { get; private set; }
        public static Transform Thrash { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            //Initialize level list and loads the prefabs
            _levels = new CyclingList<Level>(levelPrefabs);

            //Create the thrash
            Thrash = new GameObject("Thrash").transform;
        }

        private void Start()
        {
            LoadLevel();
        }

        public void LoadLevel(bool nextLevel = false)
        {
            if (!levelPrefabs.Any()) return;
            // if (nextLevel) PrefsManager.Instance.IncrementLevelIndex();

            ClearThrash();

            // var levelToLoad = _levels.GetElement(PrefsManager.Instance.GetLevelIndex());
            var levelToLoad = _levels.GetElement(0);
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Thrash);

            OnLevelLoaded?.Invoke();
        }

        public static void StartLevel()
        {
            if (IsPlaying) return;
            IsPlaying = true;

            OnLevelStarted?.Invoke();
        }

        public static void StopLevel(bool isSuccess)
        {
            if (!IsPlaying) return;
            IsPlaying = false;

            if (isSuccess) //level succeed
            {
                // UIManager.LevelCompletedUI.Show();
            }
            else //level failed
            {
                // UIManager.LevelFailedUI.Show();
            }

            OnLevelStopped?.Invoke(isSuccess);
        }

        private static void ClearThrash()
        {
            var count = Thrash.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
                Destroy(Thrash.GetChild(i).gameObject);
            }
        }
    }
}