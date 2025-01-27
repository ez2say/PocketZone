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

        private Slider _healthSlider;
        private HealthBarController _healthBarController;

        protected virtual void Start()
        {
            Health = _maxHealth;
            InitializeHealthBar();
            UpdateHealthUI();
        }

        private void InitializeHealthBar()
        {
 
            GameObject healthBarInstance = Instantiate(_healthBarPrefab, transform.position, Quaternion.identity);
            _healthBarController = healthBarInstance.GetComponent<HealthBarController>();
            _healthSlider = healthBarInstance.GetComponentInChildren<Slider>();               
            _healthBarController.SetTarget(transform, _healthBarOffset);

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
            if (_healthSlider != null)
                _healthSlider.value = (float)Health / _maxHealth;
        }
    }
}