using UnityEngine;

namespace PocketZone
{
    public class PlayerController : HealthController
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private SpriteRenderer[] _bodyParts;
        [SerializeField] private ShootController _shootController;

        private bool _isFacingRight = true;
        private IMoveHandler _moveHandler;
        private JoystickController _joystick;


        public void SetMoveHandler(IMoveHandler moveHandler)
        {
            _moveHandler = moveHandler;
        }

        public void SetJoystick(JoystickController joystick)
        {
            _joystick = joystick;
        }

        private void Update()
        {
            HandleMovement();
            UpdateShootDirection();
        }

        private void HandleMovement()
        {
            _moveHandler.HandleMovement(transform, _joystick, _speed, ref _isFacingRight, _bodyParts, _shootController);
        }

        private void UpdateShootDirection()
        {
            if (_shootController != null)
            {
                _shootController.UpdateShootDirection(_isFacingRight);
            }
        }
    }
}