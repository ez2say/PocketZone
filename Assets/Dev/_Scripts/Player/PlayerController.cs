using UnityEngine;

namespace PocketZone
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private JoystickController _joystick;
        [SerializeField] private float _speed = 5f;

        private void Update()
        {
            if (_joystick == null)
                return;

            Vector2 moveInput = _joystick.GetInput();

            if (float.IsNaN(moveInput.x) || float.IsNaN(moveInput.y))
            {
                moveInput = Vector2.zero;
            }

            Debug.Log($"Player Move Input: {moveInput}");

            transform.position += (Vector3)moveInput * _speed * Time.deltaTime;
        }
    }
}