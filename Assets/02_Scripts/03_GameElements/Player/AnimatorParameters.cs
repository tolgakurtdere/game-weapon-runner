using UnityEngine;

namespace TK.Manager
{
    public static class AnimatorParameters
    {
        public struct Triggers
        {
            public static readonly int Idle = Animator.StringToHash("Idle");
            public static readonly int Run = Animator.StringToHash("Run");
            public static readonly int Fire = Animator.StringToHash("Fire");
            public static readonly int Die = Animator.StringToHash("Die");
        }
    }
}