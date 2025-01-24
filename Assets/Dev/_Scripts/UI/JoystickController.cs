using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PocketZone
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Joystick Settings")]
        [SerializeField] private Image _joystickBackground;

        [SerializeField] private Image _joystickHandle;

        private Vector2 _inputVector;

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag called"); // Отладочное сообщение

            Vector2 pos;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position,
                eventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / _joystickBackground.rectTransform.sizeDelta.x);
                pos.y = (pos.y / _joystickBackground.rectTransform.sizeDelta.y);

                _inputVector = new Vector2(pos.x * 2, pos.y * 2);
                _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

                _joystickHandle.rectTransform.anchoredPosition = new Vector2(
                    _inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
                    _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2)
                );

                Debug.Log($"Joystick Input: {_inputVector}"); // Отладочное сообщение с текущим вводом
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown called"); // Отладочное сообщение
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp called"); // Отладочное сообщение
            _inputVector = Vector2.zero;
            _joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
        }

        public Vector2 GetInput()
        {
            return _inputVector;
        }
    }
}