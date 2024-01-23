using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TK.Manager;
using UnityEngine;

namespace WeaponRunner.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private enum MovementType
        {
            CannotMove,
            Translate
        }

        [SerializeField, Required] private Joystick joystick;
        [SerializeField, Required] private Transform splineFollower;
        [ShowInInspector, ReadOnly] private MovementType _movementType = MovementType.CannotMove;
        private const int LOCAL_POSITION_X_BORDER = 2;
        private Tween _movementTween;
        private bool _canMove = true;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStarted += OnLevelStarted;
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.OnLevelStarted -= OnLevelStarted;
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            StopMovement();
            joystick.OnPointerUp(null);
        }

        private void Update()
        {
            if (!_canMove) return;

            var joystickDirection = joystick.Direction;
            Move(joystickDirection);
        }

        private void OnLevelLoaded()
        {
            var tr = transform;

            _movementType = MovementType.CannotMove;
            splineFollower.DOKill();
            tr.DOKill();

            splineFollower.position = Vector3.zero;
            splineFollower.rotation = Quaternion.identity;
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
        }

        private void OnLevelStarted()
        {
            StartMovement();
        }


        private void Move(Vector2 delta)
        {
            switch (_movementType)
            {
                case MovementType.CannotMove:
                    return;
                case MovementType.Translate:
                    Translate(delta);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Translate(Vector2 delta)
        {
            var targetLocalPos = transform.localPosition;
            targetLocalPos.x =
                Mathf.Clamp(targetLocalPos.x + delta.x, -LOCAL_POSITION_X_BORDER, LOCAL_POSITION_X_BORDER);

            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetLocalPos,
                5 * Time.deltaTime);
        }

        private void StartMovement()
        {
            _movementType = MovementType.Translate;
            _movementTween = splineFollower.DOMoveZ(100, 1f)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .OnComplete(() => { LevelManager.StopLevel(true); });
        }

        private void StopMovement()
        {
            _movementTween?.Kill();
            _movementType = MovementType.CannotMove;
        }
    }
}