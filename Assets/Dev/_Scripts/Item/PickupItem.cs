using UnityEngine;

namespace PocketZone
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private InventoryController inventoryController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (inventoryController.AddItem(item))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}