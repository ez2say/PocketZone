using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private JoystickController _joystick;
        [SerializeField] private HealthController _healthController;

        public void Initialize()
        {
            IMoveHandler moveHandler = new MoveHandler();

            _playerController.SetMoveHandler(moveHandler);

            _playerController.SetJoystick(_joystick);

            _playerController.SetHealthController(_healthController);
        }
    }   
}
