using System;

namespace PocketZone
{
    public interface IInventoryView
    {
        void UpdateSlotUI(int slotIndex, Item item, int count);
        void ShowButtons(int slotIndex);
        void HideButtons();
        void ToggleInventory(bool isActive);

        event Action<int, SlotAction> SlotInteraction;
        event Action<bool> ToggleInventoryInteraction;
        event Action RemoveOneInteraction;
        event Action RemoveAllInteraction;
    }
}