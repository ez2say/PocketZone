using System.Collections.Generic;
using UnityEngine;

namespace PocketZone
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _itemPrefabs;
        private float _dropOffsetRange;

        public ItemDrop(List<GameObject> itemPrefabs, float dropOffsetRange)
        {
            _itemPrefabs = itemPrefabs;
            _dropOffsetRange = dropOffsetRange;
        }

        public void DropItems(Vector2 enemyPosition)
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
                    item.transform.position = enemyPosition + randomOffset;
                }
            }
        }
    }
}