using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

namespace PocketZone
{
    public class EnemyController : Entity
    {
        [Header("Enemy Settings")]
        [SerializeField] private float _detectionRadius = 5f;
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private List<GameObject> _itemPrefabs;
        [SerializeField] private float _dropOffsetRange = 0f;

        [Header("Combat Settings")]
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _attackCooldown = 1f;

        [Header("Knockback Settings")]
        [SerializeField] private float _knockbackDistance = 1f;
        [SerializeField] private float _knockbackDuration = 0.2f;

        private bool _isKnockback;
        
        private bool _canAttack = true;
        private HealthController _playerHealth;
        private Transform _playerTransform;
        private HealthController _healthController;
        private bool _isPlayerInRange = false;

        protected void Start()
        {
            _healthController = GetComponent<HealthController>();
            if (_healthController != null)
            {
                _healthController.OnDeath += OnEnemyDeath;
            }

            if (_playerTransform == null)
            {
                Debug.LogWarning("Player Transform is not assigned!");
            }
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            if (_playerTransform == null)
                return;

            CheckPlayerDistance();
            
            if (_isPlayerInRange)
            {
                MoveTowardsPlayer();
            }
        }

        private void CheckPlayerDistance()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
            _isPlayerInRange = distanceToPlayer <= _detectionRadius;
        }

        private void MoveTowardsPlayer()
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            transform.Translate(direction * _moveSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player") && _canAttack)
            {
                TryDealDamage(other.gameObject);
            }
        }

        private void TryDealDamage(GameObject player)
        {
            if (player.TryGetComponent(out HealthController health))
            {
                health.TakeDamage(_damage);
                StartCoroutine(AttackCooldown());
                StartCoroutine(Knockback());
                StartCoroutine(FlashPlayer(player));
            }
        }

        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }

        private IEnumerator FlashPlayer(GameObject player)
        {
            var spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = originalColor;
            }
        }

        private void OnEnemyDeath()
        {
            DropAllItems();
        }

        private void DropAllItems()
        {
            if (_itemPrefabs.Count > 0)
            {
                foreach (var itemPrefab in _itemPrefabs)
                {
                    Vector2 randomOffset = new Vector2(
                        Random.Range(-_dropOffsetRange, _dropOffsetRange),
                        Random.Range(-_dropOffsetRange, _dropOffsetRange)
                    );

                    var item = Instantiate(itemPrefab);
                    item.transform.position = (Vector2)transform.position + randomOffset;
                }
            }
        }

        private void OnDestroy()
        {
            if (_healthController != null)
            {
                _healthController.OnDeath -= OnEnemyDeath;
            }
        }

        private IEnumerator Knockback()
        {
            _isKnockback = true;
            
            Vector2 knockbackDirection = (transform.position - _playerTransform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position + knockbackDirection * _knockbackDistance;
            
            float elapsedTime = 0f;
            Vector2 startPosition = transform.position;

            while (elapsedTime < _knockbackDuration)
            {
                transform.position = Vector2.Lerp(
                    startPosition, 
                    targetPosition, 
                    elapsedTime / _knockbackDuration
                );
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isKnockback = false;
        }
    }
}