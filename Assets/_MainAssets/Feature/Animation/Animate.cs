using UnityEngine;

namespace Animation
{
    public abstract class Animate: MonoBehaviour, IAnimate
    {
        [SerializeField] protected float animationDuration = 0.15f;
        [SerializeField] protected float scaleUpSize = 1.2f;
        protected Vector3 originalSize;

        [SerializeField] protected float speed = 1f;


        public abstract void PlayAnimation();
    }
}