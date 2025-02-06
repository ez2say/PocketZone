using System.Collections;
using UnityEngine;

namespace PocketZone
{
    public class Knockback
    {
        private Transform _playerTransform;
        private float _knockbackDistance;
        private float _knockbackDuration;

        public Knockback(float knockbackDistance, float knockbackDuration)
        {
            _knockbackDistance = knockbackDistance;
            _knockbackDuration = knockbackDuration;
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public IEnumerator ApplyKnockback(Vector2 enemyPosition, Transform enemyTransform)
        {
            if (_playerTransform == null)
                yield break;

            Vector2 knockbackDirection = ((Vector2)_playerTransform.position - enemyPosition).normalized;
            Vector2 targetPosition = enemyPosition + knockbackDirection * _knockbackDistance;

            float elapsedTime = 0f;
            while (elapsedTime < _knockbackDuration)
            {
                enemyTransform.position = Vector2.Lerp(enemyPosition, targetPosition, elapsedTime / _knockbackDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}