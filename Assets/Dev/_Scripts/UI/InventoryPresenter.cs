using System;

namespace PocketZone
{
    public class InventoryPresenter
    {
        private readonly InventoryModel _model;
        private readonly IInventoryView _view;

        public InventoryPresenter(InventoryModel model, IInventoryView view)
        {
            _model = model;
            _view = view;

            SubscribeToViewEvents();
            UpdateView();
        }

        public bool AddItem(Item item)
        {
            bool result = _model.AddItem(item);
            UpdateView();
            return result;
        }

        private void SubscribeToViewEvents()
        {
            _view.SlotInteraction += OnSlotInteraction;
            _view.ToggleInventoryInteraction += OnToggleInventoryInteraction;
            _view.RemoveOneInteraction += OnRemoveOneInteraction;
            _view.RemoveAllInteraction += OnRemoveAllInteraction;
        }

        private void OnSlotInteraction(int slotIndex, SlotAction action)
        {
            if (action == SlotAction.Select)
            {
                _model.SetActiveSlotIndex(slotIndex);
                _view.ShowButtons(slotIndex);
            }
            else if (action == SlotAction.Deselect)
            {
                _model.SetActiveSlotIndex(-1);
                _view.HideButtons();
            }
        }

        private void OnToggleInventoryInteraction(bool isActive)
        {
            _view.ToggleInventory(isActive);
        }

        private void OnRemoveOneInteraction()
        {
            int activeSlotIndex = _model.GetActiveSlotIndex();
            if (activeSlotIndex != -1)
            {
                _model.RemoveOneItem(activeSlotIndex);
                UpdateView();
            }
        }

        private void OnRemoveAllInteraction()
        {
            int activeSlotIndex = _model.GetActiveSlotIndex();
            if (activeSlotIndex != -1)
            {
                _model.RemoveAllItems(activeSlotIndex);
                UpdateView();
            }
        }

        private void UpdateView()
        {
            for (int i = 0; i < _model.SlotCount; i++)
            {
                var slotData = _model.GetSlot(i);
                _view.UpdateSlotUI(i, slotData.Item, slotData.Count);
            }
        }
    }
}