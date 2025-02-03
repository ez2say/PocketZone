using UnityEngine;

namespace PocketZone
{
    public class UIManager : MonoBehaviour, IInventory
    {
        [SerializeField] private InventoryView _inventoryView;

        private InventoryModel _inventoryModel;
        private InventoryPresenter _inventoryPresenter;

        public InventorySlot[] Slots => _inventoryView.Slots;

        public void Initialize()
        {
            _inventoryModel = new InventoryModel(_inventoryView.Slots.Length);

            _inventoryPresenter = new InventoryPresenter(_inventoryModel, _inventoryView);

            _inventoryView.Initialize(_inventoryPresenter);
        }

        public bool AddItem(Item item)
        {
            return _inventoryPresenter.AddItem(item);
        }

        public IInventory GetInventory()
        {
            return this;
        }
    }
}