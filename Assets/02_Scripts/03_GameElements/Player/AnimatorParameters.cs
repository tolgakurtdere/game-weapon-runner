using UnityEngine;

namespace TK.Manager
{
    public static class AnimatorParameters
    {
        public static class Triggers
        {
            public static readonly int Walk = Animator.StringToHash("Walk");
            public static readonly int Run = Animator.StringToHash("Run");
            public static readonly int Die = Animator.StringToHash("Die");
        }

        public static class Bools
        {
            public static readonly int IsWalking = Animator.StringToHash("IsWalking");
            public static readonly int IsFiring = Animator.StringToHash("IsFiring");
            public static readonly int IsDead = Animator.StringToHash("IsDead");
        }

        public static class Floats
        {
            public static readonly int WalkingSpeed = Animator.StringToHash("WalkingSpeed");
        }
    }
}