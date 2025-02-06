using UnityEngine.UI;
using System;
using UnityEngine;

namespace PocketZone
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item Item;
        public int Count;
        public Image Icon;
        public Text CountText;
        public int Index;
        private ISlotClickHandler _clickHandler;

        public void Initialize(ISlotClickHandler clickHandler, int index)
        {
            _clickHandler = clickHandler;
            Index = index;

            if (Icon == null)
            {
                Debug.LogError("Icon is not assigned for inventory slot at index " + index);
                return;
            }
            Button slotButton = Icon.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(OnSlotClick);
            }
        }

        public void UpdateUI()
        {
            if (Item != null)
            {
                if (Icon != null)
                {
                    Icon.sprite = Item.Icon;
                    Icon.enabled = true;
                }

                if (CountText != null)
                {
                    if (Count > 1)
                    {
                        CountText.text = Count.ToString();
                        CountText.enabled = true;
                    }
                    else
                    {
                        CountText.enabled = false;
                    }
                }
            }
            else
            {
                Icon.enabled = false;
                CountText.enabled = false;
            }
        }

        private void OnSlotClick()
        {
            _clickHandler?.OnSlotClick(this);
        }
    }
}