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
            LevelManager.OnLevelStarted += OnLevelStarted;
            PlayerHealthController.OnPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.OnLevelStarted -= OnLevelStarted;
            PlayerHealthController.OnPlayerDied -= OnPlayerDied;
        }

        private void OnLevelLoaded(int levelNo)
        {
            Animator.SetLayerWeight(1, 1);
            Animator.SetTrigger(AnimatorParameters.Triggers.Idle);
        }

        private void OnLevelStarted(int levelNo)
        {
            Animator.SetTrigger(AnimatorParameters.Triggers.Run);
            Animator.SetTrigger(AnimatorParameters.Triggers.Fire);
        }

        private void OnPlayerDied()
        {
            Animator.SetLayerWeight(1, 0);
            Animator.SetTrigger(AnimatorParameters.Triggers.Die);
        }
    }
}