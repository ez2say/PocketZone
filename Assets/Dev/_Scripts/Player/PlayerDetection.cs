using UnityEngine;

namespace PocketZone
{
    public class PlayerDetection
    {
        private Transform _playerTransform;
        private float _detectionRadius;

        public bool IsPlayerInRange { get; private set; }

        public PlayerDetection(float detectionRadius)
        {
            _detectionRadius = detectionRadius;
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void UpdateDetection(Vector2 enemyPosition)
        {
            if (_playerTransform == null)
                return;

            float distanceToPlayer = Vector2.Distance(enemyPosition, _playerTransform.position);
            IsPlayerInRange = distanceToPlayer <= _detectionRadius;
        }
    }
}