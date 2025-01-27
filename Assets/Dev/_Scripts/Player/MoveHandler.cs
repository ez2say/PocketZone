using UnityEngine;

namespace PocketZone
{
    public class MoveHandler : IMoveHandler
    {
        public void HandleMovement(Transform transform, JoystickController joystick, float speed, ref bool isFacingRight, SpriteRenderer[] bodyParts, ShootController shootController)
        {
            if (joystick == null)
                return;

            Vector2 moveInput = joystick.GetInput();

            if (float.IsNaN(moveInput.x) || float.IsNaN(moveInput.y))
            {
                moveInput = Vector2.zero;
            }

            transform.position += (Vector3)moveInput * speed * Time.deltaTime;

            if (moveInput.x > 0 && !isFacingRight)
            {
                FlipSprites(bodyParts, false, ref isFacingRight, shootController);
            }
            else if (moveInput.x < 0 && isFacingRight)
            {
                FlipSprites(bodyParts, true, ref isFacingRight, shootController);
            }
        }

        private void FlipSprites(SpriteRenderer[] bodyParts, bool flipX, ref bool isFacingRight, ShootController shootController)
        {
            foreach (var part in bodyParts)
            {
                part.flipX = flipX;
            }
            isFacingRight = !flipX;
            UpdateShootDirection(shootController, isFacingRight);
        }

        private void UpdateShootDirection(ShootController shootController, bool isFacingRight)
        {
            if (shootController != null)
            {
                shootController.UpdateShootDirection(isFacingRight);
            }
        }
    }
}