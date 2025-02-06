using UnityEngine;

namespace PocketZone
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _maxDistance = 20f;

        private ObjectPool _objectPool;
        private Vector2 _startPosition;

        public void Initialize(ObjectPool objectPool)
        {
            _objectPool = objectPool;
            _startPosition = transform.position;

            Debug.Log($"Пуля создана на позиции {_startPosition}");
        }

        private void Update()
        {
            float distanceTraveled = Vector2.Distance(_startPosition, transform.position);
            Debug.Log($"Дистанция пули {distanceTraveled}");
            if (distanceTraveled >= _maxDistance)
            {
                Debug.Log($"Пуля должна возвратиться в пул");
                ReturnToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                TryDealDamage(collision);
                ReturnToPool();
            }
        }

        private void TryDealDamage(Collider2D enemyCollider)
        {
            if (enemyCollider.TryGetComponent(out HealthController health))
            {
                health.TakeDamage(_damage);
            }
        }

        private void ReturnToPool()
        {
            _objectPool.ReturnObjectToPool(gameObject);
        }

        
    }
}