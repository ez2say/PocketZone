using UnityEngine;

namespace PocketZone
{
    public interface IDamageable
    {
        int Health { get; set; }

        void TakeDamage(int damage);

        void Die();
    }
}

