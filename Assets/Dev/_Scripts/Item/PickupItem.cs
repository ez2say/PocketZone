using UnityEngine;

namespace PocketZone
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private Item item;
        private IInventory _inventory;

        private void Start()
        {
            _inventory = GameManager.Instance.GetInventory();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (_inventory.AddItem(item))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}