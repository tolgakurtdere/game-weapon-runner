using UnityEngine;
using Cinemachine;

namespace TK.Utility
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class CinemachineLocker : CinemachineExtension
    {
        [SerializeField] private bool lockX;
        [SerializeField] private bool lockY;
        [SerializeField] private bool lockZ;
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                if (vcam.Follow == null) return;
                var pos = state.RawPosition;

                if (lockX) pos.x = x;
                if (lockY) pos.y = y;
                if (lockZ) pos.z = z;
                state.RawPosition = pos;
            }
        }
    }
}