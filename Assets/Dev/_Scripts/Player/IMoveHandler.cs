using UnityEngine;

namespace PocketZone
{
    public interface IMoveHandler
    {
        void HandleMovement(Transform transform, JoystickController joystick, float speed, ref bool isFacingRight, SpriteRenderer[] bodyParts, ShootController shootController);
    }
}