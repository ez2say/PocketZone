using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{
    public class InventoryController : IInteractable, ISlotClickHandler,IButtonHandler, IInventory
    {
        private GameObject _inventoryPanel;
        private InventorySlot[] _slots;
        private Button _removeOneButton;
        private Button _removeAllButton;

        private InventorySlot _selectedSlot;

        public InventoryController(GameObject inventoryPanel, InventorySlot[] slots, Button removeOneButton, Button removeAllButton)
        {
            _inventoryPanel = inventoryPanel;
            _slots = slots;
            _removeOneButton = removeOneButton;
            _removeAllButton = removeAllButton;

            Initialize();
        }
        
        public void OnButtonClick()
        {
            Interact();
        }

        public void Interact()
        {
            ToggleInventory();
        }

        public void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);

            if (!_inventoryPanel.activeSelf)
            {
                HideButtons();
                _selectedSlot = null;
            }
        }

        public bool AddItem(Item item)
        {
            foreach (var slot in _slots)
            {
                if (slot.Item != null && slot.Item.ID == item.ID && slot.Count < slot.Item.MaxStack)
                {
                    slot.Count++;
                    slot.UpdateUI();
                    return true;
                }
            }

            foreach (var slot in _slots)
            {
                if (slot.Item == null)
                {
                    slot.Item = item;
                    slot.Count = 1;
                    slot.UpdateUI();
                    return true;
                }
            }

            return false;
        }

        public void OnSlotClick(InventorySlot slot)
        {
            if (_selectedSlot == slot)
            {
                HideButtons();
                _selectedSlot = null;
            }
            else
            {
                _selectedSlot = slot;
                ShowButtons(slot);
                Debug.Log("Выбран слот " + (slot.Index + 1));
            }
        }

        private void Initialize()
        {
            _inventoryPanel.SetActive(true);

            HideButtons();

            _removeOneButton.onClick.AddListener(OnButtonClick);
            _removeAllButton.onClick.AddListener(OnButtonClick);

            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].Initialize(this, i);
            }

            _inventoryPanel.SetActive(false);
        }

        private void RemoveOneItem()
        {
            if (_selectedSlot != null && _selectedSlot.Item != null && _selectedSlot.Count > 0)
            {
                _selectedSlot.Count--;

                if (_selectedSlot.Count == 0)
                {
                    _selectedSlot.Item = null;
                }

                _selectedSlot.UpdateUI();
                HideButtons();
                _selectedSlot = null;
            }
        }

        private void RemoveAllItems()
        {
            if (_selectedSlot != null && _selectedSlot.Item != null)
            {
                _selectedSlot.Item = null;
                _selectedSlot.Count = 0;
                _selectedSlot.UpdateUI();
                HideButtons();
                _selectedSlot = null;
            }
        }

        private void ShowButtons(InventorySlot slot)
        {
            _removeOneButton.gameObject.SetActive(true);

            if (slot.Count > 1)
            {
                _removeAllButton.gameObject.SetActive(true);
            }
            else
            {
                _removeAllButton.gameObject.SetActive(false);
            }
        }

        private void HideButtons()
        {
            _removeOneButton.gameObject.SetActive(false);
            _removeAllButton.gameObject.SetActive(false);
        }
    }
}