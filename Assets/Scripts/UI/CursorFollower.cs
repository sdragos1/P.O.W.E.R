using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CursorFollower : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            if (_canvas == null)
                _canvas = GetComponentInParent<Canvas>();
        }

        private void Update()
        {
            if (RobotSelectionManager.Instance.SelectedRobot == Types.RobotType.None)
            {
                _image.enabled = false;
                return;
            }

            _image.enabled = true;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
                out Vector2 localPoint
            );

            _image.rectTransform.localPosition = localPoint;

            _image.sprite = RobotSelectionManager.Instance.GetSpriteFor(RobotSelectionManager.Instance.SelectedRobot);
        }
    }
}