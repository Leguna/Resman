using DG.Tweening;
using Touch;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AnimateWhenClicked : MonoBehaviour, ITouchable
{
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private float scaleUpSize = 1.2f;
    private Vector3 _originalSize;

    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        _originalSize = transform.localScale;
    }

    public void OnTouch()
    {
        AnimateScaleWithDotween();
    }

    private void AnimateScaleWithDotween()
    {
        transform.DOScale(_originalSize * scaleUpSize, animationDuration / speed).OnComplete(() =>
        {
            transform.DOScale(_originalSize, animationDuration);
        });
    }
}