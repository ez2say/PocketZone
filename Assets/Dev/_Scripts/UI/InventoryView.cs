using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class InventoryView : MonoBehaviour, ISlotClickHandler
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventorySlot[] _slots;
        [SerializeField] private Button _removeOneButton;
        [SerializeField] private Button _removeAllButton;
        [SerializeField] private Button _toggleInventoryButton;

        protected InventoryPresenter _presenter;

        private int _activeSlotIndex = -1;

        public InventorySlot[] Slots => _slots;

        public void Initialize(InventoryPresenter presenter)
        {
            _presenter = presenter;
            _toggleInventoryButton.onClick.AddListener(ToggleInventory);
            _removeOneButton.onClick.AddListener(RemoveOneItem);
            _removeAllButton.onClick.AddListener(RemoveAllItems);

            _inventoryPanel.SetActive(false);

            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].Initialize(this, i);
            }
        }

        public void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeInHierarchy);
            
            if(!_inventoryPanel.activeInHierarchy)
            {
                HideButtons();
            }
        }

        public void ShowButtons(int slotIndex)
        {
            _removeOneButton.gameObject.SetActive(true);
            if (_presenter.GetItemCount(slotIndex) > 1)
            {
                _removeAllButton.gameObject.SetActive(true);
            }
            else
            {
                _removeAllButton.gameObject.SetActive(false);
            }
        }

        public void HideButtons()
        {
            _removeOneButton.gameObject.SetActive(false);
            _removeAllButton.gameObject.SetActive(false);
        }

        public void OnSlotClick(InventorySlot slot)
        {
            int slotIndex = System.Array.IndexOf(Slots, slot);
            if(_activeSlotIndex == slotIndex)
            {
                _activeSlotIndex = -1;
                HideButtons();
            }
            else if (slotIndex != -1 && slot.Item != null)
            {
                _activeSlotIndex = slotIndex;
                ShowButtons(slotIndex);
                _presenter.OnSlotClick(slotIndex);
            }
            else
            {
                _activeSlotIndex = -1;
                HideButtons();
            }
        }

        public void UpdateSlotUI(int slotIndex, Item item, int count)
        {
            Slots[slotIndex].Item = item;
            Slots[slotIndex].Count = count;
            Slots[slotIndex].UpdateUI();
        }

        public void RemoveOneItem()
        {
            if (_activeSlotIndex != -1)
            {
                _presenter.RemoveOneItem(_activeSlotIndex);
                UpdateActiveSlotUI(_activeSlotIndex);
            }
        }

        public void RemoveAllItems()
        {
            if (_activeSlotIndex != -1)
            {
                _presenter.RemoveAllItems(_activeSlotIndex);
                UpdateActiveSlotUI(_activeSlotIndex);
            }
        }

        private void UpdateActiveSlotUI(int slotIndex)
        {
            var slotData = _presenter.GetSlotData(slotIndex);
            UpdateSlotUI(slotIndex, slotData.Item, slotData.Count);

            if (slotData.Item == null && _activeSlotIndex == slotIndex)
            {
                _activeSlotIndex = -1;
                HideButtons();
            }
        }
    }
}