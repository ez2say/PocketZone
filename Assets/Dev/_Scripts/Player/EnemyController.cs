using UnityEngine;
using System.Collections;

namespace PocketZone
{
    public class EnemyController : Entity
    {
        public enum State { Idle = 0, Chasing = 1, Back = 2, Returning }

        [SerializeField] private Vector2 _localDetectionCenter = Vector2.zero;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Vector2 _defaultPosition;
        [SerializeField] private float _detectionRange = 5f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _backStepDistance = 2f;
        [SerializeField] private int _attackDamage = 1;
        [SerializeField] private float _returnThreshold = 0.1f;

        private Transform _playerTransform;
        private Vector3 _startPosition;
        private State _currentState;
        private bool _isAttackCooldown;
        private IDamageable _playerDamageable;

        private void Awake()
        {
            _currentState = State.Idle;

            _defaultPosition = transform.position;
        }


        private void Start()
        {
            InitializePlayerReference();
        }

        private void InitializePlayerReference()
        {
            if (GameManager.Instance == null || GameManager.Instance.PlayerController == null)
            {
                Debug.LogError("Player reference not found!");
                enabled = false;
                return;
            }
            
            _playerTransform = GameManager.Instance.PlayerController.transform;
            
            if (!GameManager.Instance.PlayerController.TryGetComponent(out _playerDamageable))
            {
                Debug.LogError("Player doesn't have IDamageable component!");
                enabled = false;
            }
        }

        private void Update()
        {
            if (CanSeePlayer())
            {
                Debug.Log("Враг видит игрока без препятствий!");
            }
            if (_playerTransform == null) return;

            if (_currentState == State.Returning)
            {
                ReturnToStart();
                return;
            }

            DistanceToPlayer();

            switch (_currentState)
            {
                case State.Idle:
                    if (DistanceToPlayer() <= _detectionRange)
                        _currentState = State.Chasing;
                    break;

                case State.Chasing:
                    if (DistanceToPlayer() > _detectionRange)
                    {
                        _currentState = State.Returning;
                    }
                    else if (DistanceToPlayer() <= _attackRange)
                    {
                        StartAttack();
                    }
                    else
                    {
                        ChasePlayer();
                    }
                    break;

                case State.Back:
                    break;
            }
        }

        private float DistanceToPlayer()
        {
            return Vector2.Distance(transform.position, _playerTransform.position);
        }

        private void ChasePlayer()
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            _rb.velocity = direction * Speed;
        }

        private void StartAttack()
        {
            if (!_isAttackCooldown)
            {
                _currentState = State.Back;
                _isAttackCooldown = true;
                StartCoroutine(BackStep());
                
                if (_playerDamageable != null)
                {
                    _playerDamageable.TakeDamage(_attackDamage);
                }
            }
        }

        private void ReturnToStart()
        {
            Vector2 direction = ((Vector2)_startPosition - (Vector2)transform.position).normalized;
            _rb.velocity = direction * Speed;

            if (Vector2.Distance(transform.position, _startPosition) < _returnThreshold)
            {
                _rb.velocity = Vector2.zero;
                _currentState = State.Idle;
            }
        }

        private IEnumerator BackStep()
        {
            Vector2 backDirection = ((Vector2)transform.position - (Vector2)_playerTransform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position + backDirection * _backStepDistance;
            
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                _rb.velocity = backDirection * Speed;
                yield return null;
            }
            
            _rb.velocity = Vector2.zero;
            _isAttackCooldown = false;
            _currentState = State.Chasing;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_isAttackCooldown)
            {
                StartAttack();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 detectionCenter = transform.TransformPoint(_localDetectionCenter);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detectionCenter, _detectionRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionCenter, _attackRange);
        }

        public bool CanSeePlayer()
        {
            if (_playerTransform == null) return false;

            Vector2 direction = (Vector2)_playerTransform.position - (Vector2)transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _detectionRange);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                return true;
            }

            return false;
        }
    }
}