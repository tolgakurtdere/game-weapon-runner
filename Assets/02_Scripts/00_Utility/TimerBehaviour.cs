using System;
using UnityEngine;

namespace TK.Utility
{
    public class TimerBehaviour : MonoBehaviour
    {
        private float _seconds = -1f;
        private Action _onTimeUp;
        private Action<float> _onUpdate;

        private void Awake()
        {
            enabled = false;
        }

        public void StartTimer(float seconds, Action onTimeUp, bool resume = false, Action<float> onUpdate = null)
        {
            if (seconds <= 0) return;

            if (!resume || _seconds <= 0)
            {
                _seconds = seconds;
            }

            _onTimeUp = onTimeUp;
            _onUpdate = onUpdate;
            enabled = true;
        }

        public void StopTimer()
        {
            enabled = false;
            _onTimeUp = null;
            _onUpdate = null;
        }

        private void Update()
        {
            if (_seconds <= 0) return;

            _onUpdate?.Invoke(_seconds);
            _seconds -= Time.deltaTime;
            if (_seconds <= 0)
            {
                _onTimeUp?.Invoke();
                StopTimer();
            }
        }
    }
}