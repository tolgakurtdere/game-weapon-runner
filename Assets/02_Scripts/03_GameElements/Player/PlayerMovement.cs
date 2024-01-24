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

        private void OnLevelStopped(int levelNo, bool isSuccess)
        {
            StopMovement();
            joystick.OnPointerUp(null);
        }

        private void Update()
        {
            Move(joystick.Direction);
        }

        private void OnLevelLoaded(int levelNo)
        {
            _movementType = MovementType.CannotMove;
            splineFollower.DOKill();
            transform.DOKill();

            splineFollower.position = Vector3.zero;
            splineFollower.rotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        private void OnLevelStarted(int levelNo)
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
                4 * Time.deltaTime);
        }

        private void StartMovement()
        {
            // This part is not well planned, I prefer to use one of the spline packages in general
            var upgradeCounter = 0;
            _movementType = MovementType.Translate;
            _movementTween = splineFollower.DOMoveZ(8, 1f)
                .SetRelative()
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .SetLoops(12, LoopType.Incremental)
                .OnStepComplete(() =>
                {
                    if (upgradeCounter >= 8)
                    {
                        return;
                    }

                    UIManager.Instance.UpgradeUI.Show();
                    upgradeCounter++;
                })
                .OnComplete(() => { LevelManager.Instance.StopLevel(true); });
        }

        private void StopMovement()
        {
            _movementTween?.Kill();
            _movementType = MovementType.CannotMove;
        }
    }
}