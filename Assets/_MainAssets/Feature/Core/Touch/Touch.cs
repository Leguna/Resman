using UnityEngine;

namespace Touch
{
    public class Touch : MonoBehaviour
    {
        private TouchInputAction _touchInputAction;
        private Camera _camera;

        private Vector2 _lastTapPos;
        private IOnDoubleTap _lastTapObj;

        private void Awake()
        {
            _camera = Camera.main;
            _touchInputAction = new TouchInputAction();
            _touchInputAction.Touch.Tap.started += _ => Tap();
            _touchInputAction.Touch.DoubleTap.started += _ => OnDoubleTapStart();
            _touchInputAction.Touch.DoubleTap.performed += _ => OnDoubleTap();
        }

        private void OnDoubleTapStart()
        {
            var pos = _touchInputAction.Touch.TapPos.ReadValue<Vector2>();
            var ray = _camera.ScreenPointToRay(pos);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == null) return;
            _lastTapObj = hit.collider.GetComponent<IOnDoubleTap>();
        }

        private void OnEnable()
        {
            _touchInputAction.Enable();
        }

        private void OnDisable()
        {
            _touchInputAction.Disable();
        }

        private void Tap()
        {
            var pos = _touchInputAction.Touch.TapPos.ReadValue<Vector2>();
            var ray = _camera.ScreenPointToRay(pos);
            var hits = new RaycastHit2D[2];
            Physics2D.RaycastNonAlloc(ray.origin, ray.direction, hits);
            _lastTapPos = pos;
            foreach (var hit in hits)
            {
                if (hit.collider == null) return;
                var touchable = hit.collider.GetComponents<ITouchable>();
                foreach (var t in touchable)
                    t.OnTouch();
            }
        }

        private void OnDoubleTap()
        {
            var pos = _touchInputAction.Touch.TapPos.ReadValue<Vector2>();
            if (Vector2.Distance(pos, _lastTapPos) > 100) return;

            if (_lastTapObj == null) return;
            var ray = _camera.ScreenPointToRay(pos);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return;
            var touchable = hit.collider.GetComponent<IOnDoubleTap>();

            if (_lastTapObj != touchable) return;
            touchable?.OnDoubleTap();
            _lastTapObj = null;
        }
    }
}