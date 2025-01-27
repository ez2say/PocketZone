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

        [SerializeField] private Slider _healthSlider;

        [SerializeField] private HealthBarController _healthBarController;

        protected virtual void Start()
        {
            Health = _maxHealth;
            UpdateHealthUI();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                Die();
            }
            UpdateHealthUI();
        }

        public virtual void Die()
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        private void UpdateHealthUI()
        {
            _healthSlider.value = (float)Health / _maxHealth;
            _healthBarController.SetTarget(transform);
        }
        
    }
}

