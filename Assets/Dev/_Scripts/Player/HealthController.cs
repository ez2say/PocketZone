using UnityEngine;
using UnityEngine.UI;
using System;

namespace PocketZone
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;
        public int Health { get; set; }

        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private Vector3 _healthBarOffset = new Vector3(0, 1f, 0);

        [SerializeField] private HealthBarController _healthBarController;


        private void Start()
        {
            Health = _maxHealth;
            Debug.Log($"{Health}");
            _healthBarController.Initialize();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            UpdateHealthUI();
        }

        public virtual void Die()
        {
            OnDeath?.Invoke();
            if (_healthBarController != null)
                Destroy(_healthBarController.gameObject);
            Destroy(gameObject);
        }

        private void UpdateHealthUI()
        {
            if (_healthBarController == null)
            {
                Debug.LogError("HealthBarController is not assigned in " + gameObject.name);
                return;
            }

            _healthBarController.DecreaseHealthBar((float)Health / _maxHealth);
        }
    }
}