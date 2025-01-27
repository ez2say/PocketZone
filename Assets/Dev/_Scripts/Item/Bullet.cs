using UnityEngine;

namespace PocketZone
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _lifeTime = 3f;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                TryDealDamage(collision);
                Destroy(gameObject);
            }
        }

        private void TryDealDamage(Collider2D enemyCollider)
        {
            if (enemyCollider.TryGetComponent(out HealthController health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}