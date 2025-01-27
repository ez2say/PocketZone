using UnityEngine;
using System.Collections;

namespace PocketZone
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private BoxCollider2D _spawnArea;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private float _minDistanceFromPlayer = 2f;

        private int _currentEnemies = 0;
        private Coroutine _spawnCoroutine;

        public void Initialize()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            _spawnCoroutine = StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (_currentEnemies < _maxEnemies)
                {
                    Vector2 spawnPos = GetRandomPositionInPolygon();
                    if (spawnPos != Vector2.zero)
                    {
                        GameObject enemyPrefab = _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)];
                        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                        SubscribeToEnemyDeath(enemy);
                        _currentEnemies++;

                        Debug.Log($"Enemy spawned at position: {spawnPos}");
                    }
                }
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private Vector2 GetRandomPositionInPolygon()
        {
            if (_spawnArea == null)
            {
                return Vector2.zero;
            }

            Bounds bounds = _spawnArea.bounds;

            Vector2 randomPoint;
            int attempts = 100;

            for (int i = 0; i < attempts; i++)
            {
                randomPoint = new Vector2(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    UnityEngine.Random.Range(bounds.min.y, bounds.max.y)
                );

                if (_spawnArea.OverlapPoint(randomPoint) && !IsTooCloseToPlayer(randomPoint))
                {
                    return randomPoint;
                }
            }

            return Vector2.zero;
        }

        private bool IsTooCloseToPlayer(Vector2 position)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(position, player.transform.position);
                return distanceToPlayer < _minDistanceFromPlayer;
            }
            return false;
        }

        private void SubscribeToEnemyDeath(GameObject enemy)
        {
            if (enemy.TryGetComponent(out HealthController healthController))
            {
                healthController.OnDeath += () => _currentEnemies--;
            }
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (_spawnArea != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(_spawnArea.bounds.center, _spawnArea.bounds.size);
            }
        }
    }
}