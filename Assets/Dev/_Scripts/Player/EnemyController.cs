using UnityEngine;
using System.Collections.Generic;

namespace PocketZone
{
    public class EnemyController : Entity
    {
        [SerializeField] private List<GameObject> _itemPrefabs;
        [SerializeField] private float _dropOffsetRange = 0.5f;

        private HealthController _healthController;

        protected void Start()
        {
            _healthController = GetComponent<HealthController>();
            if (_healthController != null)
            {
                _healthController.OnDeath += OnEnemyDeath;
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
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-_dropOffsetRange, _dropOffsetRange),
                        Random.Range(-_dropOffsetRange, _dropOffsetRange),
                        0
                    );

                    Instantiate(itemPrefab, transform.position + randomOffset, Quaternion.identity);
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
    }
}