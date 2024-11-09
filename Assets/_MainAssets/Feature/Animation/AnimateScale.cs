using DG.Tweening;

namespace Animation
{
    public class AnimateScale : Animate
    {
        protected virtual void Awake()
        {
            originalSize = transform.localScale;
        }

        private void AnimateScaleWithDotween()
        {
            transform.DOScale(originalSize * scaleUpSize, animationDuration / speed).OnComplete(() =>
            {
                transform.DOScale(originalSize, animationDuration);
            });
        }

        public override void PlayAnimation()
        {
            AnimateScaleWithDotween();
        }
    }
}