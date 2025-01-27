using UnityEngine;

namespace PocketZone
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                HealthController enemyHealth = collision.GetComponent<HealthController>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                }

                Destroy(gameObject);
            }
        }
    }
}