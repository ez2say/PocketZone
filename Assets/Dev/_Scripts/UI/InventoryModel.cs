using System.Collections.Generic;

namespace PocketZone
{
    public class InventoryModel
    {
        public int SlotCount => _slots.Count;
        private List<InventorySlot> _slots;
        private int _activeSlotIndex = -1;

        public InventoryModel(int slotCount)
        {
            _slots = new List<InventorySlot>();
            for (int i = 0; i < slotCount; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }

        public bool AddItem(Item item)
        {
            if (item == null) return false;

            foreach (var slot in _slots)
            {
                if (slot.Item != null && slot.Item.ID == item.ID && slot.Count < item.MaxStack)
                {
                    slot.Count++;
                    return true;
                }
            }

            foreach (var slot in _slots)
            {
                if (slot.Item == null)
                {
                    slot.Item = item;
                    slot.Count = 1;
                    return true;
                }
            }

            return false;
        }

        public void RemoveOneItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count) return;

            var slot = _slots[slotIndex];
            if (slot.Item != null && slot.Count > 0)
            {
                slot.Count--;
                if (slot.Count == 0)
                {
                    slot.Item = null;
                }
            }
        }

        public void RemoveAllItems(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count) return;

            var slot = _slots[slotIndex];
            slot.Item = null;
            slot.Count = 0;
        }

        public InventorySlot GetSlot(int index)
        {
            return (index >= 0 && index < _slots.Count) ? _slots[index] : null;
        }

        public int GetItemCount(int slotIndex)
        {
            return (slotIndex >= 0 && slotIndex < _slots.Count) ? _slots[slotIndex]?.Count ?? 0 : 0;
        }

        public void SetActiveSlotIndex(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _slots.Count)
            {
                _activeSlotIndex = slotIndex;
            }
            else
            {
                _activeSlotIndex = -1;
            }
        }

        public int GetActiveSlotIndex()
        {
            return _activeSlotIndex;
        }

        public InventorySlot GetActiveSlot()
        {
            return (GetActiveSlotIndex() >= 0) ? _slots[_activeSlotIndex] : null;
        }
    }
}