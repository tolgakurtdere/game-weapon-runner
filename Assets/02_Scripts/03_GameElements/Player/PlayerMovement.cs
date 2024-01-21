using DG.Tweening;
using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    [RequireComponent(typeof(PlayerWrapper))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private bool _canMove = true;
        private PlayerWrapper _playerWrapper;
        private Joystick _joystick;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        private void Awake()
        {
            _playerWrapper = GetComponent<PlayerWrapper>();

            _animator = _playerWrapper.AnimatorController.Animator;
            _joystick = GameManager.Joystick;
        }

        private void OnLevelLoaded()
        {
            var tr = transform;
            tr.DOKill();
            tr.position = Vector3.zero;
            tr.rotation = Quaternion.identity;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _joystick.OnPointerUp(null);
            _animator.SetBool(AnimatorParameters.Bools.IsWalking, isSuccess);
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

            // var joystickDirection = _joystick.Direction;
        }
    }
}