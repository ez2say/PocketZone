using UnityEngine;

namespace PocketZone
{
    public class Movement
    {
        private Transform _target;
        private float _moveSpeed;

        public Movement(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void MoveTowards(Vector2 currentPosition)
        {
            if (_target == null)
                return;

            Vector2 direction = ((Vector2)_target.position - (Vector2)currentPosition).normalized;
            Vector2 newPosition = currentPosition + direction * _moveSpeed * Time.deltaTime;
            currentPosition = newPosition;
        }
    }
}