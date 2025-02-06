using System.Collections;
using UnityEngine;

namespace PocketZone
{
    public class Combat
    {
        private int _damage;
        private float _attackCooldown;
        private bool _canAttack;
        private MonoBehaviour _context; // Контекст для доступа к Unity API

        public Combat(int damage, float attackCooldown, MonoBehaviour context)
        {
            _damage = damage;
            _attackCooldown = attackCooldown;
            _canAttack = true;
            _context = context; // Сохраняем контекст
        }

        public void TryDealDamage(GameObject player)
        {
            if (!_canAttack || player == null)
                return;

            if (player.TryGetComponent(out HealthController health))
            {
                health.TakeDamage(_damage);

                if (_context != null)
                {
                    _context.StartCoroutine(AttackCooldown());
                }
            }
        }

        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }
    }
}