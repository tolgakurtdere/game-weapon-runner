using DG.Tweening;
using UnityEngine;

namespace TK.UI.Tutorial
{
    public class DragTutorial : MonoBehaviour
    {
        [SerializeField] private RectTransform hand;
        [SerializeField] private RectTransform[] points;
        [SerializeField] private float duration = 3f;
        private Vector2 _initHandPos;
        private Tween _handTween;

        private void Awake()
        {
            _initHandPos = hand.anchoredPosition;
        }

        private void OnEnable()
        {
            var path = new Vector3[points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                path[i] = points[i].localPosition;
            }

            _handTween = hand.DOLocalPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        private void OnDisable()
        {
            _handTween?.Kill();
            hand.anchoredPosition = _initHandPos;
        }
    }
}