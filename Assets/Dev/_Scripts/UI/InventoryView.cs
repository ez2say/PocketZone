using System;
using UnityEngine;
using UnityEngine.UI;

namespace PocketZone
{   
    public enum SlotAction
    {
        Select,
        Deselect
    }
    public class InventoryView : MonoBehaviour, IInventoryView, ISlotClickHandler
    {   
        public event Action<int, SlotAction> SlotInteraction;
        public event Action<bool> ToggleInventoryInteraction;
        public event Action RemoveOneInteraction;
        public event Action RemoveAllInteraction;

        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventorySlot[] _slots;
        [SerializeField] private Button _removeOneButton;
        [SerializeField] private Button _removeAllButton;
        [SerializeField] private Button _toggleInventoryButton;

        private int _activeSlotIndex = -1;

        public InventorySlot[] Slots => _slots;

        public void Initialize()
        {
            Subscribe();
            
            _inventoryPanel.SetActive(false);

            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].Initialize(this, i);
            }
        }

        private void Subscribe()
        {
            _toggleInventoryButton.onClick.AddListener(() => OnToggleInventoryClicked());
            _removeOneButton.onClick.AddListener(() => OnRemoveOneButtonClicked());
            _removeAllButton.onClick.AddListener(() => OnRemoveAllButtonClicked());
        }

        public void UpdateSlotUI(int slotIndex, Item item, int count)
        {
            Slots[slotIndex].Item = item;
            Slots[slotIndex].Count = count;
            Slots[slotIndex].UpdateUI();
        }

        public void ShowButtons(int slotIndex)
        {
            _removeOneButton.gameObject.SetActive(true);
            if (Slots[slotIndex].Count > 1)
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

        public void ToggleInventory(bool isActive)
        {
            _inventoryPanel.SetActive(isActive);
            if (!isActive)
            {
                HideButtons();
            }
        }

        public void OnSlotClick(InventorySlot slot)
        {
            int slotIndex = Array.IndexOf(Slots, slot);
            if (_activeSlotIndex == slotIndex)
            {
                _activeSlotIndex = -1;
                HideButtons();
                SlotInteraction?.Invoke(slotIndex, SlotAction.Deselect);
            }
            else if (slotIndex != -1 && slot.Item != null)
            {
                _activeSlotIndex = slotIndex;
                ShowButtons(slotIndex);
                SlotInteraction?.Invoke(slotIndex, SlotAction.Select);
            }
            else
            {
                _activeSlotIndex = -1;
                HideButtons();
            }
        }

        private void OnToggleInventoryClicked()
        {
            ToggleInventoryInteraction?.Invoke(!_inventoryPanel.activeInHierarchy);
        }

        private void OnRemoveOneButtonClicked()
        {
            if (_activeSlotIndex != -1)
            {
                RemoveOneInteraction?.Invoke();
            }
        }

        private void OnRemoveAllButtonClicked()
        {
            if (_activeSlotIndex != -1)
            {
                RemoveAllInteraction?.Invoke();
            }
        }
    }

    
}