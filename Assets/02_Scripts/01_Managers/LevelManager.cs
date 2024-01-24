using System;
using System.Collections.Generic;
using System.Linq;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class LevelManager : SingletonBehaviour<LevelManager>
    {
        public static event Action<int> OnLevelLoaded;
        public static event Action<int> OnLevelStarted;
        public static event Action<int, bool> OnLevelStopped;

        [SerializeField] private List<Level> levelPrefabs = new();
        private CyclingList<Level> _levels;
        private int _currentLevelIndex;

        public static bool IsPlaying { get; set; }
        public static Level CurrentLevel { get; private set; }
        public static Transform Thrash { get; private set; }
        public int CurrentLevelNo => _currentLevelIndex + 1;

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

        private void Update()
        {
            // Actually, not a good approach. If user tap on an UI element such as shop button, level still get started.
            // Waiting first joystick click event would be more proper.
            // Fortunately, we do not have any clickable UI element right now :)
            if (Input.GetMouseButtonDown(0))
            {
                StartLevel();
            }
        }

        private void LoadLevel(bool nextLevel = false)
        {
            if (!levelPrefabs.Any()) return;
            if (nextLevel) _currentLevelIndex++;

            ClearThrash();

            var levelToLoad = _levels.GetElement(_currentLevelIndex);
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Thrash);

            enabled = true;
            OnLevelLoaded?.Invoke(CurrentLevelNo);
        }

        private void StartLevel()
        {
            if (IsPlaying) return;
            IsPlaying = true;
            enabled = false;

            OnLevelStarted?.Invoke(CurrentLevelNo);
        }

        public void StartNextLevel()
        {
            LoadLevel(true);
        }

        public void RestartLevel()
        {
            LoadLevel();
        }

        public void StopLevel(bool isSuccess)
        {
            if (!IsPlaying) return;
            IsPlaying = false;

            OnLevelStopped?.Invoke(CurrentLevelNo, isSuccess);
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