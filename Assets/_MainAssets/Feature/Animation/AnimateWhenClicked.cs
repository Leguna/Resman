using Touch;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Collider2D))]
    public class AnimateWhenClicked : AnimateScale, ITouchable
    {
        public void OnTouch()
        {
            PlayAnimation();
        }
    }
}