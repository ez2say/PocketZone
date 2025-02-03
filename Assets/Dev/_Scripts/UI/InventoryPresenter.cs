using System;

namespace PocketZone
{
    public class InventoryPresenter
    {
        private InventoryModel _model;
        private InventoryView _view;

        public InventoryPresenter(InventoryModel model, InventoryView view)
        {
            _model = model;
            _view = view;
        }

        public bool AddItem(Item item)
        {
            bool result = _model.AddItem(item);
            UpdateView();
            return result;
        }

        public void OnSlotClick(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _model.SlotCount)
            {
                var slotData = _model.GetSlot(slotIndex);
                if (slotData.Item != null)
                {
                    _view.ShowButtons(slotIndex);
                }
            }
        }

        public void RemoveOneItem(int slotIndex)
        {
            _model.RemoveOneItem(slotIndex);
            UpdateView();

        }

        public void RemoveAllItems(int slotIndex)
        {
            _model.RemoveAllItems(slotIndex);
            UpdateView();
        }

        public int GetItemCount(int slotIndex)
        {
            return _model.GetItemCount(slotIndex);
        }

        private void UpdateView()
        {
            for (int i = 0; i < _model.SlotCount; i++)
            {
                var slotData = _model.GetSlot(i);
                _view.UpdateSlotUI(i, slotData.Item, slotData.Count);
            }
        }

        public InventorySlot GetSlotData(int slotIndex)
        {
            return _model.GetSlot(slotIndex);
        }
    }
}