using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;

        public Animator Animator
        {
            get
            {
                if (!_animator) _animator = GetComponentInChildren<Animator>();
                return _animator;
            }
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            PlayerHealthController.OnPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            PlayerHealthController.OnPlayerDied -= OnPlayerDied;
        }

        private void OnLevelLoaded()
        {
            Animator.SetBool(AnimatorParameters.Bools.IsDead, false);
        }

        private void OnPlayerDied()
        {
            Animator.SetBool(AnimatorParameters.Bools.IsDead, true);
        }
    }
}