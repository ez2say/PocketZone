using UnityEngine;

namespace PocketZone
{
    public class PlayerController : Entity
    {
        [SerializeField] private SpriteRenderer[] _bodyParts;
        [SerializeField] private ShootController _shootController;

        private bool _isFacingRight = true;
        private IMoveHandler _moveHandler;
        private JoystickController _joystick;
        private HealthController _healthController;


        public void SetMoveHandler(IMoveHandler moveHandler)
        {
            _moveHandler = moveHandler;
        }

        public void SetJoystick(JoystickController joystick)
        {
            _joystick = joystick;
        }

        public void SetHealthController(HealthController healthController)
        {
            _healthController = healthController;
        }

        private void Update()
        {
            HandleMovement();
            UpdateShootDirection();
        }

        private void HandleMovement()
        {
            _moveHandler.HandleMovement(transform, _joystick, Speed, ref _isFacingRight, _bodyParts, _shootController);
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